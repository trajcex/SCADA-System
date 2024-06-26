﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseManager.ServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Tag", Namespace="http://schemas.datacontract.org/2004/07/CoreService.Model")]
    [System.SerializableAttribute()]
    public partial class Tag : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TagNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Address {
            get {
                return this.AddressField;
            }
            set {
                if ((object.ReferenceEquals(this.AddressField, value) != true)) {
                    this.AddressField = value;
                    this.RaisePropertyChanged("Address");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TagName {
            get {
                return this.TagNameField;
            }
            set {
                if ((object.ReferenceEquals(this.TagNameField, value) != true)) {
                    this.TagNameField = value;
                    this.RaisePropertyChanged("TagName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.ICore")]
    public interface ICore {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICore/SaveTags", ReplyAction="http://tempuri.org/ICore/SaveTagsResponse")]
        void SaveTags();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICore/SaveTags", ReplyAction="http://tempuri.org/ICore/SaveTagsResponse")]
        System.Threading.Tasks.Task SaveTagsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICore/GetTags", ReplyAction="http://tempuri.org/ICore/GetTagsResponse")]
        DatabaseManager.ServiceReference.Tag[] GetTags();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICore/GetTags", ReplyAction="http://tempuri.org/ICore/GetTagsResponse")]
        System.Threading.Tasks.Task<DatabaseManager.ServiceReference.Tag[]> GetTagsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICoreChannel : DatabaseManager.ServiceReference.ICore, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CoreClient : System.ServiceModel.ClientBase<DatabaseManager.ServiceReference.ICore>, DatabaseManager.ServiceReference.ICore {
        
        public CoreClient() {
        }
        
        public CoreClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CoreClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CoreClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CoreClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void SaveTags() {
            base.Channel.SaveTags();
        }
        
        public System.Threading.Tasks.Task SaveTagsAsync() {
            return base.Channel.SaveTagsAsync();
        }
        
        public DatabaseManager.ServiceReference.Tag[] GetTags() {
            return base.Channel.GetTags();
        }
        
        public System.Threading.Tasks.Task<DatabaseManager.ServiceReference.Tag[]> GetTagsAsync() {
            return base.Channel.GetTagsAsync();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.IAuthentication")]
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
    public interface IAuthenticationChannel : DatabaseManager.ServiceReference.IAuthentication, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AuthenticationClient : System.ServiceModel.ClientBase<DatabaseManager.ServiceReference.IAuthentication>, DatabaseManager.ServiceReference.IAuthentication {
        
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
