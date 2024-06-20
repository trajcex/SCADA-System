using CoreService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using SimulationLibrary;

namespace CoreService
{

    public class TagProcessing : ITagProcessing,IMonitoring
    {
        
        private static List<ICallback> callbacks = new List<ICallback>();
        private static Dictionary<string, Thread> tagMap = new Dictionary<string, Thread>();

        private TagProcessing() { }

        public TagProcessing(List<Tag> digitalInputTags,List<Tag> analogInputTags)
        {
            
            foreach (Tag tag in digitalInputTags)
            {
                DigitalInputTag digitalInputTag = (DigitalInputTag)tag;
                if (digitalInputTag.Scan) StartTag(tag);
            }
            foreach (Tag tag in analogInputTags)
            {
                AnalogInputTag analogInputTag = (AnalogInputTag)tag;
                if (analogInputTag.Scan) StartTag(tag);
            }
        }
        public void StopTag(string uid)
        {
            throw new NotImplementedException();
        }

        public void StartTag(Tag tag)
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
                    string message = $"Tag Name: {tag.TagName}, Value: {value.ToString() ?? "null"}";
                    Write(message);
                    Thread.Sleep(digitalInputTag.ScanTime*1000);
                }
                else if (tag.GetType() == typeof(AnalogInputTag))
                {
                    AnalogInputTag analogInputTag = (AnalogInputTag) tag;
                    double value = GetFunction(analogInputTag.Address);
                    string message = $"Tag Name: {tag.TagName}, Value: {value.ToString() ?? "null"}";
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
