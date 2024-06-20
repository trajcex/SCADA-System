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
using Org.BouncyCastle.Utilities;

namespace CoreService
{

    public class TagProcessing : ITagProcessing,IMonitoring, IAlarmMonitoring
    {
        
        private static List<ICallback> callbacksTrending = new List<ICallback>();
        private static List<ICallback> callbacksAlarm = new List<ICallback>();
        private static Dictionary<string, (Thread Thread, CancellationTokenSource Cts )> tagMap = new Dictionary<string, (Thread, CancellationTokenSource)>();
        private static UserContextDB _contextDb = new UserContextDB();
        private readonly object locker = new object();

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
        public void StopTag(string tagName)
        {
            tagMap[tagName].Cts.Cancel();
            tagMap[tagName].Thread.Join();
            tagMap.Remove(tagName);
        }
        
        public void StartTag(Tag tag)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Thread thread = new Thread(() => ProcessTag(tag,token));
            tagMap[tag.TagName] = (thread,cts);
            thread.Start();
        }

        private void ProcessTag(Tag tag, CancellationToken token) {
            try
            {
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    if (tag.GetType() == typeof(DigitalInputTag))
                    {

                        DigitalInputTag digitalInputTag = (DigitalInputTag) tag;
                        double value = GetFunction(digitalInputTag.Address);
                        DateTime now = DateTime.UtcNow;
                        DateTimeOffset dateTimeOffset = new DateTimeOffset(now);
                        long milliseconds = dateTimeOffset.ToUnixTimeMilliseconds();
                        string message = $"Tag Name: {tag.TagName}, Value: {value.ToString() ?? "null"}";
                        SaveTagValue(new TagValue(value.ToString(), "DI", milliseconds.ToString(), tag.TagName));
                        Write(callbacksTrending,message);
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
                        writeAlarms(analogInputTag, (int)value);
                        Write(callbacksTrending,message);
                        Thread.Sleep(analogInputTag.ScanTime*1000);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private void writeAlarms(AnalogInputTag tag, int currentValue)
        {
            foreach(Alarm alarm in tag.Alarms) { 
                if(alarm.Type == AlarmType.LOW)
                {
                    if(currentValue < alarm.Border)
                    {
                        string alarmMessage = "ALARM LOW TYPE, TAG: " + tag.TagName + " UNIT: " + tag.Units + ", CURRENT VALUE: " + currentValue +  " IS UNDER " + alarm.Border;
                        writeAlarmMessageByPriority(alarm.Priority, alarmMessage);
                    }
                }else if(alarm.Type == AlarmType.HIGH)
                {
                    if(currentValue > alarm.Border)
                    {
                        string alarmMessage = "ALARM HIGH TYPE, TAG: " + tag.TagName + " UNIT: " + tag.Units + ", CURRENT VALUE: " + currentValue +  " IS ABOVE " + alarm.Border;
                        writeAlarmMessageByPriority(alarm.Priority, alarmMessage);
                    }
                }
            }
        }
        private void writeAlarmMessageByPriority(int priority, string alarmMessage)
        {
            for(int i = 0; i < priority; i++)
            {
                Write1(callbacksAlarm, alarmMessage);
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
        private void Write(List<ICallback> callbacks,string message)
        {
            foreach (var item in callbacks)
            {
                item.Message(message);
            }
        }
        private void Write1(List<ICallback> callbacks, string message)
        {
            foreach (var item in callbacks)
            {
                item.Message(message);
            }
        }

        public void InitSubTrending()
        {
            callbacksTrending.Add(OperationContext.Current.GetCallbackChannel<ICallback>());
        }
        public void InitSubAlarm()
        {
            callbacksAlarm.Add(OperationContext.Current.GetCallbackChannel<ICallback>());
        }

    }
}
