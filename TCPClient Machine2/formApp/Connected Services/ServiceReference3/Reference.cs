﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace formApp.ServiceReference3 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference3.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InsertUserDetails", ReplyAction="http://tempuri.org/IService1/InsertUserDetailsResponse")]
        string InsertUserDetails(string email, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InsertUserDetails", ReplyAction="http://tempuri.org/IService1/InsertUserDetailsResponse")]
        System.Threading.Tasks.Task<string> InsertUserDetailsAsync(string email, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CheckUser", ReplyAction="http://tempuri.org/IService1/CheckUserResponse")]
        string CheckUser(string email, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CheckUser", ReplyAction="http://tempuri.org/IService1/CheckUserResponse")]
        System.Threading.Tasks.Task<string> CheckUserAsync(string email, string pass);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : formApp.ServiceReference3.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<formApp.ServiceReference3.IService1>, formApp.ServiceReference3.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string InsertUserDetails(string email, string pass) {
            return base.Channel.InsertUserDetails(email, pass);
        }
        
        public System.Threading.Tasks.Task<string> InsertUserDetailsAsync(string email, string pass) {
            return base.Channel.InsertUserDetailsAsync(email, pass);
        }
        
        public string CheckUser(string email, string pass) {
            return base.Channel.CheckUser(email, pass);
        }
        
        public System.Threading.Tasks.Task<string> CheckUserAsync(string email, string pass) {
            return base.Channel.CheckUserAsync(email, pass);
        }
    }
}
