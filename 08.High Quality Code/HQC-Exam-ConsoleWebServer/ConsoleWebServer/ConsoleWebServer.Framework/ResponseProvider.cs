namespace ConsoleWebServer.Framework
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Reflection;

    public class ResponseProvider : IResponseProvider
    {
        private const string HeadRequest = "head";
        private const string OptionsRequest = "options";
        private const string ControllerName = "Controller";

        public HttpResponse GetResponse(string requestAsString)
        {
            HttpRequest request;
            try
            {
                var requestParser = new HttpRequest();
                request = requestParser.Parse(requestAsString);
            }
            catch (NullReferenceException)
            {
                return new HttpResponse(new Version(1, 1), HttpStatusCode.NotImplemented, "Request cannot be handled.");
            }
            catch (Exception ex)
            {
                return new HttpResponse(new Version(1, 1), HttpStatusCode.BadRequest, ex.Message);
            }

            var response = this.Process(request);
            return response;
        }

        private HttpResponse Process(HttpRequest request)
        {
            if (request.Method.ToLower() == HeadRequest)
            {
                return new HttpResponse(request.ProtocolVersion, HttpStatusCode.OK, string.Empty);
            }
            else if (request.Method.ToLower() == OptionsRequest)
            {
                var routes =
                    Assembly.GetEntryAssembly()
                        .GetTypes()
                        .Where(x => x.Name.EndsWith(ControllerName) && typeof(Controller).IsAssignableFrom(x))
                        .Select(
                            x =>
                                new
                                {
                                    x.Name,
                                    Methods = x.GetMethods().Where(m => m.ReturnType == typeof(IActionResult))
                                })
                        .SelectMany(x => x.Methods
                            .Select(
                                m =>
                                    string.Format("/{0}/{1}/{{parameter}}", x.Name.Replace(ControllerName, string.Empty), m.Name)))
                        .ToList();

                return new HttpResponse(request.ProtocolVersion, HttpStatusCode.OK, string.Join(Environment.NewLine, routes));
            }
            else if (new StaticFileHandler().CanHandle(request))
            {
                return new StaticFileHandler().Handle(request);
            }
            else if (request.ProtocolVersion.Major < 3)
            {
                HttpResponse response;
                try
                {
                    var controller = this.CreateController(request);
                    var actionInvoker = new NewActionInvoker();
                    var actionResult = actionInvoker.InvokeAction(controller, request.Action);
                    response = actionResult.GetResponse();
                }
                catch (HttpNotFound exception)
                {
                    response = new HttpResponse(request.ProtocolVersion, HttpStatusCode.NotFound, exception.Message);
                }
                catch (Exception exception)
                {
                    response = new HttpResponse(request.ProtocolVersion, HttpStatusCode.InternalServerError, exception.Message);
                }

                return response;
            }
            else
            {
                return new HttpResponse(request.ProtocolVersion, HttpStatusCode.NotImplemented, "Request cannot be handled.");
            }
        }

        private Controller CreateController(HttpRequest request)
        {
            var controllerClassName = request.Action.ControllerName + ControllerName;
            var type = Assembly.GetEntryAssembly().GetTypes()
                .FirstOrDefault(x => x.Name.ToLower() == controllerClassName.ToLower() &&
                                typeof(Controller).IsAssignableFrom(x));
            if (type == null)
            {
                throw new HttpNotFound(
                    string.Format("Controller with name {0} not found!", controllerClassName));
            }

            var instance = (Controller)Activator.CreateInstance(type, request);
            return instance;
        }
    }
}