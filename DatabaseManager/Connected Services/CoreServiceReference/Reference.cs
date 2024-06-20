﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseManager.CoreServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CoreServiceReference.IAuthentication")]
    public interface IAuthentication {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/Registration", ReplyAction="http://tempuri.org/IAuthentication/RegistrationResponse")]
        bool Registration(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/Registration", ReplyAction="http://tempuri.org/IAuthentication/RegistrationResponse")]
        System.Threading.Tasks.Task<bool> RegistrationAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/Login", ReplyAction="http://tempuri.org/IAuthentication/LoginResponse")]
        string Login(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/Login", ReplyAction="http://tempuri.org/IAuthentication/LoginResponse")]
        System.Threading.Tasks.Task<string> LoginAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/Logout", ReplyAction="http://tempuri.org/IAuthentication/LogoutResponse")]
        bool Logout(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/Logout", ReplyAction="http://tempuri.org/IAuthentication/LogoutResponse")]
        System.Threading.Tasks.Task<bool> LogoutAsync(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/IsUserAuthenticated", ReplyAction="http://tempuri.org/IAuthentication/IsUserAuthenticatedResponse")]
        bool IsUserAuthenticated(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthentication/IsUserAuthenticated", ReplyAction="http://tempuri.org/IAuthentication/IsUserAuthenticatedResponse")]
        System.Threading.Tasks.Task<bool> IsUserAuthenticatedAsync(string token);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAuthenticationChannel : DatabaseManager.CoreServiceReference.IAuthentication, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuthenticationClient : System.ServiceModel.ClientBase<DatabaseManager.CoreServiceReference.IAuthentication>, DatabaseManager.CoreServiceReference.IAuthentication {
        
        public AuthenticationClient() {
        }
        
        public AuthenticationClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AuthenticationClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthenticationClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AuthenticationClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Registration(string username, string password) {
            return base.Channel.Registration(username, password);
        }
        
        public System.Threading.Tasks.Task<bool> RegistrationAsync(string username, string password) {
            return base.Channel.RegistrationAsync(username, password);
        }
        
        public string Login(string username, string password) {
            return base.Channel.Login(username, password);
        }
        
        public System.Threading.Tasks.Task<string> LoginAsync(string username, string password) {
            return base.Channel.LoginAsync(username, password);
        }
        
        public bool Logout(string token) {
            return base.Channel.Logout(token);
        }
        
        public System.Threading.Tasks.Task<bool> LogoutAsync(string token) {
            return base.Channel.LogoutAsync(token);
        }
        
        public bool IsUserAuthenticated(string token) {
            return base.Channel.IsUserAuthenticated(token);
        }
        
        public System.Threading.Tasks.Task<bool> IsUserAuthenticatedAsync(string token) {
            return base.Channel.IsUserAuthenticatedAsync(token);
        }
    }
}
