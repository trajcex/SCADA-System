using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Serialization;
using System.Web;

namespace CoreService.Model
{
    public interface IInput
    {
        string Driver { get; set; }
        int ScanTime { get; set; }
        bool Scan { get; set; }
    }

    public interface IOutput
    {
        int InitialValue { get; set; }
    }

    public interface IAnalog
    {
         int LowLimit { get; set; }
         int HighLimit { get; set; }
         string Units { get; set; }
    }
    [DataContract]
    [KnownType(typeof(DigitalInputTag))]
    [KnownType(typeof(DigitalOutputTag))]
    [KnownType(typeof(AnalogInputTag))]
    [KnownType(typeof(AnalogOutputTag))]
    public class Tag
    {
        [DataMember]
        public string TagName { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Address { get; set; }
    
        public Tag() { }

        public Tag(string tagName, string description, string address)
        {
            TagName = tagName;
            Description = description;
            Address = address;
        }
    }
    [DataContract]
    public class DigitalInputTag : Tag, IInput
    {
        [DataMember]
        public string Driver { get; set; }
        [DataMember]
        public bool Scan { get; set; }
        [DataMember]
        public int ScanTime { get; set; }

        public DigitalInputTag() { }
        public DigitalInputTag(string tagName, string description, string address,
            string driver, int scanTime, bool scan) :base(tagName,description,address)
        {
            Driver = driver;
            ScanTime = scanTime;
            Scan = scan;
        }
    }
    [DataContract]
    public class DigitalOutputTag : Tag, IOutput
    {
        [DataMember]
        public int InitialValue { get; set ; }
        public DigitalOutputTag(){ }

        public DigitalOutputTag(int initialValue,string tagName, string description, string address) 
            : base(tagName, description, address)
        {
            InitialValue = initialValue;
        }
    }
    [DataContract]
    public class AnalogInputTag : Tag, IInput, IAnalog
    {
        [DataMember]
        public string Driver { get; set; }
        [DataMember]
        public int ScanTime { get; set; }
        [DataMember]
        public bool Scan { get; set;}
        [DataMember]
        public int LowLimit {  get; set; }
        [DataMember]
        public int HighLimit { get; set; }
        [DataMember]
        public string Units { get; set; }

        public List<Alarm> Alarms { get; set; } = new List<Alarm>();

        public AnalogInputTag() { }

        public AnalogInputTag(string tagName, string description, string address,List<Alarm> alarms,
            string driver, int scanTime, bool scan, string units,int lowLimit, int highLimit) : base(tagName, description, address)
        {
            Alarms = alarms;
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Driver = driver;
            ScanTime = scanTime;
            Scan = scan;
            Units = units;
        }
    }
    [DataContract]
    public class AnalogOutputTag : Tag, IOutput, IAnalog
    {
        [DataMember]
        public int InitialValue { get; set ; }
        [DataMember]
        public int LowLimit { get; set; }
        [DataMember]
        public int HighLimit { get; set; }
        [DataMember]
        public string Units { get; set; }

        public AnalogOutputTag() { }

        public AnalogOutputTag(string tagName, string description, string address, int initalValue,
            string units, int lowLimit, int highLimit) : base(tagName, description, address)
        {
            InitialValue = initalValue; 
            LowLimit = lowLimit;
            HighLimit = highLimit;
            Units = units;
        }
    }


}