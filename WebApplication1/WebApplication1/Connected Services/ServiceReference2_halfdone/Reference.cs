﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.ServiceReference2_halfdone {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference2_halfdone.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetUpdate", ReplyAction="http://tempuri.org/IService1/GetUpdateResponse")]
        System.Data.DataTable GetUpdate();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetUpdate", ReplyAction="http://tempuri.org/IService1/GetUpdateResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> GetUpdateAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InsertUserDetails", ReplyAction="http://tempuri.org/IService1/InsertUserDetailsResponse")]
        string InsertUserDetails(string email, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/InsertUserDetails", ReplyAction="http://tempuri.org/IService1/InsertUserDetailsResponse")]
        System.Threading.Tasks.Task<string> InsertUserDetailsAsync(string email, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CheckUser", ReplyAction="http://tempuri.org/IService1/CheckUserResponse")]
        string CheckUser(string email, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CheckUser", ReplyAction="http://tempuri.org/IService1/CheckUserResponse")]
        System.Threading.Tasks.Task<string> CheckUserAsync(string email, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/getAllUsers", ReplyAction="http://tempuri.org/IService1/getAllUsersResponse")]
        System.Data.DataTable getAllUsers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/getAllUsers", ReplyAction="http://tempuri.org/IService1/getAllUsersResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> getAllUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UpdateUser", ReplyAction="http://tempuri.org/IService1/UpdateUserResponse")]
        string UpdateUser(string email, string password, int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UpdateUser", ReplyAction="http://tempuri.org/IService1/UpdateUserResponse")]
        System.Threading.Tasks.Task<string> UpdateUserAsync(string email, string password, int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DeleteUser", ReplyAction="http://tempuri.org/IService1/DeleteUserResponse")]
        string DeleteUser(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DeleteUser", ReplyAction="http://tempuri.org/IService1/DeleteUserResponse")]
        System.Threading.Tasks.Task<string> DeleteUserAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CreateMachine", ReplyAction="http://tempuri.org/IService1/CreateMachineResponse")]
        string CreateMachine(string machineName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CreateMachine", ReplyAction="http://tempuri.org/IService1/CreateMachineResponse")]
        System.Threading.Tasks.Task<string> CreateMachineAsync(string machineName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UpdateMachineName", ReplyAction="http://tempuri.org/IService1/UpdateMachineNameResponse")]
        string UpdateMachineName(string machineName, int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/UpdateMachineName", ReplyAction="http://tempuri.org/IService1/UpdateMachineNameResponse")]
        System.Threading.Tasks.Task<string> UpdateMachineNameAsync(string machineName, int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DeleteMachine", ReplyAction="http://tempuri.org/IService1/DeleteMachineResponse")]
        string DeleteMachine(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/DeleteMachine", ReplyAction="http://tempuri.org/IService1/DeleteMachineResponse")]
        System.Threading.Tasks.Task<string> DeleteMachineAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/getAllMachine", ReplyAction="http://tempuri.org/IService1/getAllMachineResponse")]
        System.Data.DataTable getAllMachine();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/getAllMachine", ReplyAction="http://tempuri.org/IService1/getAllMachineResponse")]
        System.Threading.Tasks.Task<System.Data.DataTable> getAllMachineAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : WebApplication1.ServiceReference2_halfdone.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<WebApplication1.ServiceReference2_halfdone.IService1>, WebApplication1.ServiceReference2_halfdone.IService1 {
        
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
        
        public System.Data.DataTable GetUpdate() {
            return base.Channel.GetUpdate();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> GetUpdateAsync() {
            return base.Channel.GetUpdateAsync();
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
        
        public System.Data.DataTable getAllUsers() {
            return base.Channel.getAllUsers();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> getAllUsersAsync() {
            return base.Channel.getAllUsersAsync();
        }
        
        public string UpdateUser(string email, string password, int id) {
            return base.Channel.UpdateUser(email, password, id);
        }
        
        public System.Threading.Tasks.Task<string> UpdateUserAsync(string email, string password, int id) {
            return base.Channel.UpdateUserAsync(email, password, id);
        }
        
        public string DeleteUser(int id) {
            return base.Channel.DeleteUser(id);
        }
        
        public System.Threading.Tasks.Task<string> DeleteUserAsync(int id) {
            return base.Channel.DeleteUserAsync(id);
        }
        
        public string CreateMachine(string machineName) {
            return base.Channel.CreateMachine(machineName);
        }
        
        public System.Threading.Tasks.Task<string> CreateMachineAsync(string machineName) {
            return base.Channel.CreateMachineAsync(machineName);
        }
        
        public string UpdateMachineName(string machineName, int id) {
            return base.Channel.UpdateMachineName(machineName, id);
        }
        
        public System.Threading.Tasks.Task<string> UpdateMachineNameAsync(string machineName, int id) {
            return base.Channel.UpdateMachineNameAsync(machineName, id);
        }
        
        public string DeleteMachine(int id) {
            return base.Channel.DeleteMachine(id);
        }
        
        public System.Threading.Tasks.Task<string> DeleteMachineAsync(int id) {
            return base.Channel.DeleteMachineAsync(id);
        }
        
        public System.Data.DataTable getAllMachine() {
            return base.Channel.getAllMachine();
        }
        
        public System.Threading.Tasks.Task<System.Data.DataTable> getAllMachineAsync() {
            return base.Channel.getAllMachineAsync();
        }
    }
}
