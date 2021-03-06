﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StringClient.MyStringService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MyStringService.IStringSearchingService")]
    public interface IStringSearchingService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStringSearchingService/FindStringInString", ReplyAction="http://tempuri.org/IStringSearchingService/FindStringInStringResponse")]
        int FindStringInString(string text, string searched, bool isCaseSensitive);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStringSearchingService/FindStringInString", ReplyAction="http://tempuri.org/IStringSearchingService/FindStringInStringResponse")]
        System.Threading.Tasks.Task<int> FindStringInStringAsync(string text, string searched, bool isCaseSensitive);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStringSearchingServiceChannel : StringClient.MyStringService.IStringSearchingService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StringSearchingServiceClient : System.ServiceModel.ClientBase<StringClient.MyStringService.IStringSearchingService>, StringClient.MyStringService.IStringSearchingService {
        
        public StringSearchingServiceClient() {
        }
        
        public StringSearchingServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StringSearchingServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StringSearchingServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StringSearchingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int FindStringInString(string text, string searched, bool isCaseSensitive) {
            return base.Channel.FindStringInString(text, searched, isCaseSensitive);
        }
        
        public System.Threading.Tasks.Task<int> FindStringInStringAsync(string text, string searched, bool isCaseSensitive) {
            return base.Channel.FindStringInStringAsync(text, searched, isCaseSensitive);
        }
    }
}
