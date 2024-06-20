using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Model
{
    [DataContract]
    public class Alarm
    {
        [DataMember]
        public AlarmType Type { get; set; }
        [DataMember]
        public int Priority { get; set; }
        [DataMember]
        public int Border { get; set; }
    }
    [DataContract]
    public enum AlarmType 
    {
        [EnumMember]
        LOW,
        [EnumMember]
        HIGH
    }
    
}
