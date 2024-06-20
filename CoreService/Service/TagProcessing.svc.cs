using SharedLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using SimulationLibrary;
using CoreService.Interface;

namespace CoreService.Service
{

    public class TagProcessing : ITagProcessing,IMonitoring
    {
        
        private static List<ICallback> callbacks = new List<ICallback>();
        private static Dictionary<string, Thread> tagMap = new Dictionary<string, Thread>();
        private static UserContextDB _contextDb = new UserContextDB();
        private readonly object locker = new object();

        private TagProcessing() { }

        public TagProcessing(List<Tag> digitalInputTags,List<Tag> analogInputTags)
        {
            
            foreach (Tag tag in digitalInputTags)
            {
                DigitalInputTag digitalInputTag = (DigitalInputTag)tag;
                if (digitalInputTag.Scan) startTag(tag); 
            }
            foreach (Tag tag in analogInputTags)
            {
                AnalogInputTag analogInputTag = (AnalogInputTag)tag;
                if (analogInputTag.Scan) startTag(tag);
            }
        }
        public void StopTag(string tagName)
        {
            throw new NotImplementedException();
        }

        public void StartTag(string tagName)
        {
            throw new NotImplementedException();
        }
        public void startTag(Tag tag)
        {
            Thread thread = new Thread(() => ProcessTag(tag));
            tagMap[tag.TagName] = thread;
            thread.Start();
            
        }

        private void ProcessTag(Tag tag) {
            while (true)
            {
                
                if (tag.GetType() == typeof(DigitalInputTag))
                {

                    DigitalInputTag digitalInputTag = (DigitalInputTag) tag;
                    double value = GetFunction(digitalInputTag.Address);
                    DateTime now = DateTime.UtcNow;
                    DateTimeOffset dateTimeOffset = new DateTimeOffset(now);
                    long milliseconds = dateTimeOffset.ToUnixTimeMilliseconds();
                    string message = $"Tag Name: {tag.TagName}, Value: {value.ToString() ?? "null"}";
                    SaveTagValue(new TagValue(value.ToString(), "DI", milliseconds.ToString(), tag.TagName));
                    Write(message);
                    Thread.Sleep(digitalInputTag.ScanTime*1000);
                }
                else if (tag.GetType() == typeof(AnalogInputTag))
                {
                    AnalogInputTag analogInputTag = (AnalogInputTag) tag;
                    double value = GetFunction(analogInputTag.Address);
                    DateTime now = DateTime.UtcNow;
                    DateTimeOffset dateTimeOffset = new DateTimeOffset(now);
                    long milliseconds = dateTimeOffset.ToUnixTimeMilliseconds();
                    string message = $"Tag Name: {tag.TagName}, Value: {value.ToString() ?? "null"}";
                    SaveTagValue(new TagValue(value.ToString(), "DI", milliseconds.ToString(), tag.TagName));
                    Write(message);
                    Thread.Sleep(analogInputTag.ScanTime*1000);
                }
            }
        }

        private double GetFunction(string function) 
        {
            switch (function)
            {
                case "S":
                    return SimulationDriver.ReturnValue(function);
                case "C":
                    return SimulationDriver.ReturnValue(function);
                case "R":
                    return SimulationDriver.ReturnValue(function);
                default:
                    
                    RealTimeDriver rtd = new RealTimeDriver();
                    return 0.0;
                    
            }
        }
        private void SaveTagValue(TagValue tagValue)
        {
            lock (locker)
            {
                _contextDb.TagValues.Add(tagValue);
                _contextDb.SaveChanges();

            }
        }
        private void Write(string message)
        {
            foreach (var item in callbacks)
            {
                item.Message(message);
            }
        }

        public void InitSub()
        {
            callbacks.Add(OperationContext.Current.GetCallbackChannel<ICallback>());
        }

    }
}
