﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Trending.TagProcessingServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Tag", Namespace="http://schemas.datacontract.org/2004/07/SharedLibrary.Model")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Trending.TagProcessingServiceReference.DigitalInputTag))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Trending.TagProcessingServiceReference.DigitalOutputTag))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Trending.TagProcessingServiceReference.AnalogInputTag))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Trending.TagProcessingServiceReference.AnalogOutputTag))]
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DigitalInputTag", Namespace="http://schemas.datacontract.org/2004/07/SharedLibrary.Model")]
    [System.SerializableAttribute()]
    public partial class DigitalInputTag : Trending.TagProcessingServiceReference.Tag {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DriverField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool ScanField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ScanTimeField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Driver {
            get {
                return this.DriverField;
            }
            set {
                if ((object.ReferenceEquals(this.DriverField, value) != true)) {
                    this.DriverField = value;
                    this.RaisePropertyChanged("Driver");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Scan {
            get {
                return this.ScanField;
            }
            set {
                if ((this.ScanField.Equals(value) != true)) {
                    this.ScanField = value;
                    this.RaisePropertyChanged("Scan");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ScanTime {
            get {
                return this.ScanTimeField;
            }
            set {
                if ((this.ScanTimeField.Equals(value) != true)) {
                    this.ScanTimeField = value;
                    this.RaisePropertyChanged("ScanTime");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DigitalOutputTag", Namespace="http://schemas.datacontract.org/2004/07/SharedLibrary.Model")]
    [System.SerializableAttribute()]
    public partial class DigitalOutputTag : Trending.TagProcessingServiceReference.Tag {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int InitialValueField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int InitialValue {
            get {
                return this.InitialValueField;
            }
            set {
                if ((this.InitialValueField.Equals(value) != true)) {
                    this.InitialValueField = value;
                    this.RaisePropertyChanged("InitialValue");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AnalogInputTag", Namespace="http://schemas.datacontract.org/2004/07/SharedLibrary.Model")]
    [System.SerializableAttribute()]
    public partial class AnalogInputTag : Trending.TagProcessingServiceReference.Tag {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Trending.TagProcessingServiceReference.Alarm[] AlarmsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DriverField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int HighLimitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int LowLimitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool ScanField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ScanTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UnitsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Trending.TagProcessingServiceReference.Alarm[] Alarms {
            get {
                return this.AlarmsField;
            }
            set {
                if ((object.ReferenceEquals(this.AlarmsField, value) != true)) {
                    this.AlarmsField = value;
                    this.RaisePropertyChanged("Alarms");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Driver {
            get {
                return this.DriverField;
            }
            set {
                if ((object.ReferenceEquals(this.DriverField, value) != true)) {
                    this.DriverField = value;
                    this.RaisePropertyChanged("Driver");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int HighLimit {
            get {
                return this.HighLimitField;
            }
            set {
                if ((this.HighLimitField.Equals(value) != true)) {
                    this.HighLimitField = value;
                    this.RaisePropertyChanged("HighLimit");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int LowLimit {
            get {
                return this.LowLimitField;
            }
            set {
                if ((this.LowLimitField.Equals(value) != true)) {
                    this.LowLimitField = value;
                    this.RaisePropertyChanged("LowLimit");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Scan {
            get {
                return this.ScanField;
            }
            set {
                if ((this.ScanField.Equals(value) != true)) {
                    this.ScanField = value;
                    this.RaisePropertyChanged("Scan");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ScanTime {
            get {
                return this.ScanTimeField;
            }
            set {
                if ((this.ScanTimeField.Equals(value) != true)) {
                    this.ScanTimeField = value;
                    this.RaisePropertyChanged("ScanTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Units {
            get {
                return this.UnitsField;
            }
            set {
                if ((object.ReferenceEquals(this.UnitsField, value) != true)) {
                    this.UnitsField = value;
                    this.RaisePropertyChanged("Units");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AnalogOutputTag", Namespace="http://schemas.datacontract.org/2004/07/SharedLibrary.Model")]
    [System.SerializableAttribute()]
    public partial class AnalogOutputTag : Trending.TagProcessingServiceReference.Tag {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int HighLimitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int InitialValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int LowLimitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UnitsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int HighLimit {
            get {
                return this.HighLimitField;
            }
            set {
                if ((this.HighLimitField.Equals(value) != true)) {
                    this.HighLimitField = value;
                    this.RaisePropertyChanged("HighLimit");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int InitialValue {
            get {
                return this.InitialValueField;
            }
            set {
                if ((this.InitialValueField.Equals(value) != true)) {
                    this.InitialValueField = value;
                    this.RaisePropertyChanged("InitialValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int LowLimit {
            get {
                return this.LowLimitField;
            }
            set {
                if ((this.LowLimitField.Equals(value) != true)) {
                    this.LowLimitField = value;
                    this.RaisePropertyChanged("LowLimit");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Units {
            get {
                return this.UnitsField;
            }
            set {
                if ((object.ReferenceEquals(this.UnitsField, value) != true)) {
                    this.UnitsField = value;
                    this.RaisePropertyChanged("Units");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Alarm", Namespace="http://schemas.datacontract.org/2004/07/SharedLibrary.Model")]
    [System.SerializableAttribute()]
    public partial class Alarm : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int BorderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PriorityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Trending.TagProcessingServiceReference.AlarmType TypeField;
        
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
        public int Border {
            get {
                return this.BorderField;
            }
            set {
                if ((this.BorderField.Equals(value) != true)) {
                    this.BorderField = value;
                    this.RaisePropertyChanged("Border");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Priority {
            get {
                return this.PriorityField;
            }
            set {
                if ((this.PriorityField.Equals(value) != true)) {
                    this.PriorityField = value;
                    this.RaisePropertyChanged("Priority");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Trending.TagProcessingServiceReference.AlarmType Type {
            get {
                return this.TypeField;
            }
            set {
                if ((this.TypeField.Equals(value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AlarmType", Namespace="http://schemas.datacontract.org/2004/07/SharedLibrary.Model")]
    public enum AlarmType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        LOW = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        HIGH = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TagProcessingServiceReference.ITagProcessing")]
    public interface ITagProcessing {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagProcessing/StartTag", ReplyAction="http://tempuri.org/ITagProcessing/StartTagResponse")]
        void StartTag(Trending.TagProcessingServiceReference.Tag tag);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagProcessing/StartTag", ReplyAction="http://tempuri.org/ITagProcessing/StartTagResponse")]
        System.Threading.Tasks.Task StartTagAsync(Trending.TagProcessingServiceReference.Tag tag);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagProcessing/StopTag", ReplyAction="http://tempuri.org/ITagProcessing/StopTagResponse")]
        void StopTag(string tagName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagProcessing/StopTag", ReplyAction="http://tempuri.org/ITagProcessing/StopTagResponse")]
        System.Threading.Tasks.Task StopTagAsync(string tagName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITagProcessingChannel : Trending.TagProcessingServiceReference.ITagProcessing, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TagProcessingClient : System.ServiceModel.ClientBase<Trending.TagProcessingServiceReference.ITagProcessing>, Trending.TagProcessingServiceReference.ITagProcessing {
        
        public TagProcessingClient() {
        }
        
        public TagProcessingClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TagProcessingClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TagProcessingClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TagProcessingClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void StartTag(Trending.TagProcessingServiceReference.Tag tag) {
            base.Channel.StartTag(tag);
        }
        
        public System.Threading.Tasks.Task StartTagAsync(Trending.TagProcessingServiceReference.Tag tag) {
            return base.Channel.StartTagAsync(tag);
        }
        
        public void StopTag(string tagName) {
            base.Channel.StopTag(tagName);
        }
        
        public System.Threading.Tasks.Task StopTagAsync(string tagName) {
            return base.Channel.StopTagAsync(tagName);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TagProcessingServiceReference.IMonitoring", CallbackContract=typeof(Trending.TagProcessingServiceReference.IMonitoringCallback))]
    public interface IMonitoring {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IMonitoring/InitSubTrending")]
        void InitSubTrending();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IMonitoring/InitSubTrending")]
        System.Threading.Tasks.Task InitSubTrendingAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMonitoringCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IMonitoring/Message")]
        void Message([System.ServiceModel.MessageParameterAttribute(Name="message")] string message1);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMonitoringChannel : Trending.TagProcessingServiceReference.IMonitoring, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MonitoringClient : System.ServiceModel.DuplexClientBase<Trending.TagProcessingServiceReference.IMonitoring>, Trending.TagProcessingServiceReference.IMonitoring {
        
        public MonitoringClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public MonitoringClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public MonitoringClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public MonitoringClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public MonitoringClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void InitSubTrending() {
            base.Channel.InitSubTrending();
        }
        
        public System.Threading.Tasks.Task InitSubTrendingAsync() {
            return base.Channel.InitSubTrendingAsync();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TagProcessingServiceReference.IAlarmMonitoring", CallbackContract=typeof(Trending.TagProcessingServiceReference.IAlarmMonitoringCallback))]
    public interface IAlarmMonitoring {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAlarmMonitoring/InitSubAlarm")]
        void InitSubAlarm();
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAlarmMonitoring/InitSubAlarm")]
        System.Threading.Tasks.Task InitSubAlarmAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAlarmMonitoringCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IAlarmMonitoring/Message")]
        void Message([System.ServiceModel.MessageParameterAttribute(Name="message")] string message1);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAlarmMonitoringChannel : Trending.TagProcessingServiceReference.IAlarmMonitoring, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AlarmMonitoringClient : System.ServiceModel.DuplexClientBase<Trending.TagProcessingServiceReference.IAlarmMonitoring>, Trending.TagProcessingServiceReference.IAlarmMonitoring {
        
        public AlarmMonitoringClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public AlarmMonitoringClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public AlarmMonitoringClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public AlarmMonitoringClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public AlarmMonitoringClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void InitSubAlarm() {
            base.Channel.InitSubAlarm();
        }
        
        public System.Threading.Tasks.Task InitSubAlarmAsync() {
            return base.Channel.InitSubAlarmAsync();
        }
    }
}
