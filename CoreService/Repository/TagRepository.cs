using SharedLibrary.Model;
using CoreService.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Web;
using System.Xml.Linq;

namespace CoreService.Repository
{
    
    public class TagRepository : ITagRepository
    {
        private readonly string configFilePath;
        private object integer;

        public TagRepository()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = ConfigurationManager.AppSettings["FilePath"];
            configFilePath = Path.Combine(basePath,relativePath);
        }
        public Dictionary<string,List<Tag>> GetTags()
        {
            Dictionary<string, List<Tag>> tagsDictionary = new Dictionary<string, List<Tag>>();

            XDocument xdoc = XDocument.Load(configFilePath);
            XElement root = xdoc.Element("Tags");

            foreach (XElement tagElement in root.Elements())
            {
                string tagName = tagElement.Name.LocalName;

                if (!tagsDictionary.ContainsKey(tagName))
                {
                    tagsDictionary[tagName] = new List<Tag>();
                }

                if (tagName == "DigitalInputTag")
                {
                    DigitalInputTag tag = new DigitalInputTag
                    {
                        TagName = tagElement.Attribute("tagName")?.Value ?? "",
                        Description = tagElement.Attribute("description")?.Value ?? "",
                        Address = tagElement.Attribute("address")?.Value ?? "",
                        Driver = tagElement.Attribute("driver")?.Value ?? "",
                        ScanTime = int.Parse(tagElement.Attribute("scanTime")?.Value ?? "0"),
                        Scan = bool.Parse(tagElement.Attribute("scan")?.Value ?? "false")
                    };
                    tagsDictionary[tagName].Add(tag);
                }
                else if (tagName == "DigitalOutputTag")
                {
                    DigitalOutputTag tag = new DigitalOutputTag
                    {
                        TagName = tagElement.Attribute("tagName")?.Value ?? "",
                        Description = tagElement.Attribute("description")?.Value ?? "",
                        Address = tagElement.Attribute("address")?.Value ?? "",
                        InitialValue = int.Parse(tagElement.Attribute("initialValue")?.Value ?? "0")
                    };
                    tagsDictionary[tagName].Add(tag);
                }
                else if (tagName == "AnalogInputTag")
                {
                    AnalogInputTag tag = new AnalogInputTag
                    {
                        TagName = tagElement.Attribute("tagName")?.Value ?? "",
                        Description = tagElement.Attribute("description")?.Value ?? "",
                        Address = tagElement.Attribute("address")?.Value ?? "",
                        Driver = tagElement.Attribute("driver")?.Value ?? "",
                        ScanTime = int.Parse(tagElement.Attribute("scanTime")?.Value ?? "0"),
                        Scan = bool.Parse(tagElement.Attribute("scan")?.Value ?? "false"),
                        LowLimit = int.Parse(tagElement.Attribute("lowLimit")?.Value ?? "0"),
                        HighLimit = int.Parse(tagElement.Attribute("highLimit")?.Value ?? "0"),
                        Units = tagElement.Attribute("units")?.Value ?? "",
                        Alarms = (from alarmElement in tagElement.Elements("Alarm")
                                  select new Alarm
                                  {
                                      Type = Enum.TryParse(alarmElement.Attribute("type")?.Value, true, out AlarmType alarmType) 
                                      ? alarmType : throw new Exception($"Invalid AlarmType: {alarmElement.Attribute("type")?.Value}"),
                                      Priority = int.Parse(alarmElement.Attribute("priority").Value),
                                      Border = int.Parse(alarmElement.Attribute("border").Value),
                                  }).ToList()
                    };
                    tagsDictionary[tagName].Add(tag);
                }
                else if (tagName == "AnalogOutputTag")
                {
                    AnalogOutputTag tag = new AnalogOutputTag
                    {
                        TagName = tagElement.Attribute("tagName")?.Value ?? "",
                        Description = tagElement.Attribute("description")?.Value ?? "",
                        Address = tagElement.Attribute("address")?.Value ?? "",
                        InitialValue = int.Parse(tagElement.Attribute("initialValue")?.Value ?? "0"),
                        LowLimit = int.Parse(tagElement.Attribute("lowLimit")?.Value ?? "0"),
                        HighLimit = int.Parse(tagElement.Attribute("highLimit")?.Value ?? "0"),
                        Units = tagElement.Attribute("units")?.Value ?? ""
                    };
                    tagsDictionary[tagName].Add(tag);
                }
            }
            
            return tagsDictionary;

        }

        public void SaveTags(Dictionary<string,List<Tag>> tags)
        {
            List<Tag> di = tags["DigitalInputTags"];
            List<Tag> d0 = tags["DigitalOutputTags"];
            List<Tag> ai = tags["AnalogInputTags"];
            List<Tag> ao = tags["AnalogOutputTags"];

            XElement XTags = new XElement("Tags",
            from tag in di
            select new XElement("DigitalInputTag",
                new XAttribute("tagName", tag.TagName),
                new XAttribute("description", tag.Description),
                new XAttribute("address", tag.Address),
                new XAttribute("driver", ((DigitalInputTag)tag).Driver),
                new XAttribute("scanTime", ((DigitalInputTag)tag).ScanTime),
                new XAttribute("scan", ((DigitalInputTag)tag).Scan)
            ),
            from tag in d0
            select new XElement("DigitalOutputTag",
                new XAttribute("tagName", tag.TagName),
                new XAttribute("description", tag.Description),
                new XAttribute("address", tag.Address),
                new XAttribute("initialValue", ((DigitalOutputTag)tag).InitialValue)
            ),
            from tag in ai
            select new XElement("AnalogInputTag",
                new XAttribute("tagName", tag.TagName),
                new XAttribute("description", tag.Description),
                new XAttribute("address", tag.Address),
                new XAttribute("driver", ((AnalogInputTag)tag).Driver),
                new XAttribute("scanTime", ((AnalogInputTag)tag).ScanTime),
                new XAttribute("scan", ((AnalogInputTag)tag).Scan),
                new XAttribute("lowLimit", ((AnalogInputTag)tag).LowLimit),
                new XAttribute("highLimit", ((AnalogInputTag)tag).HighLimit),
                new XAttribute("units", ((AnalogInputTag)tag).Units),
                from alarm in ((AnalogInputTag)tag).Alarms
                select new XElement("Alarm",
                    new XAttribute("type", alarm.Type),
                    new XAttribute("priority", alarm.Priority),
                    new XAttribute("border", alarm.Border)
                )
            ),
            from tag in ao
            select new XElement("AnalogOutputTag",
                new XAttribute("tagName", tag.TagName),
                new XAttribute("description", tag.Description),
                new XAttribute("address", tag.Address),
                new XAttribute("initialValue", ((AnalogOutputTag)tag).InitialValue),
                new XAttribute("lowLimit", ((AnalogOutputTag)tag).LowLimit),
                new XAttribute("highLimit", ((AnalogOutputTag)tag).HighLimit),
                new XAttribute("units", ((AnalogOutputTag)tag).Units)
            )
        );


            using (StringWriter sw = new StringWriter())
            {
                XTags.Save(sw);
                XTags.Save(configFilePath);
            }
        }
    }
}