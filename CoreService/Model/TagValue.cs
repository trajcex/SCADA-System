using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoreService.Model
{
    public class TagValue
    {
        [Key]
        public int Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public string Time { get; set; }
        public string TagName { get; set; }
        public TagValue() { }

        public TagValue(string value, string type, string time, string tagName)
        {
            Value = value;
            Type = type;
            Time = time;
            TagName = tagName;
        }

    }
}