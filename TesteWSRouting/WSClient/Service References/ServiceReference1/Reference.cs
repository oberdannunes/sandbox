﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WSClient.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.Servico1Soap")]
    public interface Servico1Soap {
        
        // CODEGEN: Generating message contract since element name HelloWorld1Result from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld1", ReplyAction="*")]
        WSClient.ServiceReference1.HelloWorld1Response HelloWorld1(WSClient.ServiceReference1.HelloWorld1Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/HelloWorld1", ReplyAction="*")]
        System.Threading.Tasks.Task<WSClient.ServiceReference1.HelloWorld1Response> HelloWorld1Async(WSClient.ServiceReference1.HelloWorld1Request request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorld1Request {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorld1", Namespace="http://tempuri.org/", Order=0)]
        public WSClient.ServiceReference1.HelloWorld1RequestBody Body;
        
        public HelloWorld1Request() {
        }
        
        public HelloWorld1Request(WSClient.ServiceReference1.HelloWorld1RequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class HelloWorld1RequestBody {
        
        public HelloWorld1RequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorld1Response {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorld1Response", Namespace="http://tempuri.org/", Order=0)]
        public WSClient.ServiceReference1.HelloWorld1ResponseBody Body;
        
        public HelloWorld1Response() {
        }
        
        public HelloWorld1Response(WSClient.ServiceReference1.HelloWorld1ResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class HelloWorld1ResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HelloWorld1Result;
        
        public HelloWorld1ResponseBody() {
        }
        
        public HelloWorld1ResponseBody(string HelloWorld1Result) {
            this.HelloWorld1Result = HelloWorld1Result;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface Servico1SoapChannel : WSClient.ServiceReference1.Servico1Soap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Servico1SoapClient : System.ServiceModel.ClientBase<WSClient.ServiceReference1.Servico1Soap>, WSClient.ServiceReference1.Servico1Soap {
        
        public Servico1SoapClient() {
        }
        
        public Servico1SoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Servico1SoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Servico1SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Servico1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        WSClient.ServiceReference1.HelloWorld1Response WSClient.ServiceReference1.Servico1Soap.HelloWorld1(WSClient.ServiceReference1.HelloWorld1Request request) {
            return base.Channel.HelloWorld1(request);
        }
        
        public string HelloWorld1() {
            WSClient.ServiceReference1.HelloWorld1Request inValue = new WSClient.ServiceReference1.HelloWorld1Request();
            inValue.Body = new WSClient.ServiceReference1.HelloWorld1RequestBody();
            WSClient.ServiceReference1.HelloWorld1Response retVal = ((WSClient.ServiceReference1.Servico1Soap)(this)).HelloWorld1(inValue);
            return retVal.Body.HelloWorld1Result;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<WSClient.ServiceReference1.HelloWorld1Response> WSClient.ServiceReference1.Servico1Soap.HelloWorld1Async(WSClient.ServiceReference1.HelloWorld1Request request) {
            return base.Channel.HelloWorld1Async(request);
        }
        
        public System.Threading.Tasks.Task<WSClient.ServiceReference1.HelloWorld1Response> HelloWorld1Async() {
            WSClient.ServiceReference1.HelloWorld1Request inValue = new WSClient.ServiceReference1.HelloWorld1Request();
            inValue.Body = new WSClient.ServiceReference1.HelloWorld1RequestBody();
            return ((WSClient.ServiceReference1.Servico1Soap)(this)).HelloWorld1Async(inValue);
        }
    }
}
