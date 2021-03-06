﻿namespace ConsoleWebServer.Framework
{
    using System.Collections.Generic;
    using System.Net;

    public class ContentActionResult : IActionResult
    {
        private readonly object model;
        private IActionResult actionResult;

        public ContentActionResult(HttpRequest request, object model, IActionResult actionResult)
            : this(request, model)
        {
            this.actionResult = actionResult;
        }

        public ContentActionResult(HttpRequest request, object model)
        {
            this.model = model;
            this.Request = request;
            this.ResponseHeaders = new List<KeyValuePair<string, string>>();
        }

        public List<KeyValuePair<string, string>> ResponseHeaders { get; set; }

        public HttpRequest Request { get; set; }

        public IActionResult ActionResult
        {
            get { return this.actionResult; }
            set { this.actionResult = value; }
        }

        public virtual HttpResponse GetResponse()
        {
            var response = new HttpResponse(this.Request.ProtocolVersion, HttpStatusCode.OK, this.model.ToString(), "text/plain; charset=utf-8");

            foreach (var responseHeader in this.ResponseHeaders)
            {
                response.AddHeader(responseHeader.Key, responseHeader.Value);
            }

            return response;
        }
    }
}