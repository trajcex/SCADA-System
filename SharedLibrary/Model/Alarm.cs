using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Id { get; set; }
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

    [DataContract]
    public class AlarmValue
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string AlarmId { get; set; }
        [DataMember]
        public AlarmType Type { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }
        [DataMember]
        public int Priority { get; set; }

    }
}
