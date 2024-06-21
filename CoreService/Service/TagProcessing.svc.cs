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
using System.IO;

namespace CoreService
{

    public class TagProcessing : ITagProcessing,IMonitoring, IAlarmMonitoring
    {
        
        private static List<ICallback> callbacksTrending = new List<ICallback>();
        private static List<ICallback> callbacksAlarm = new List<ICallback>();
        private static Dictionary<string, (Thread Thread, CancellationTokenSource Cts )> tagMap = new Dictionary<string, (Thread, CancellationTokenSource)>();
        private static UserContextDB _contextDb = new UserContextDB();
        private readonly object locker = new object();
        private static List<AlarmValue> alarmList = new List<AlarmValue>();
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
                        SaveTagValue(new TagValue(value.ToString(), "AI", milliseconds.ToString(), tag.TagName));
                        writeAlarms(analogInputTag, (int)value);
                        Write(callbacksTrending, message);
                        Thread.Sleep(analogInputTag.ScanTime*1000);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private void writeAlarmsLog(string logMessage)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            string relativePath = "AlarmLog/alarmLogs.txt";

            string configFilePath = Path.Combine(basePath, relativePath);

            string directoryPath = Path.GetDirectoryName(configFilePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (StreamWriter writer = new StreamWriter(configFilePath, true))
            {
                writer.WriteLine(logMessage);
            }
        }
        private void writeAlarms(AnalogInputTag tag, int currentValue)
        {
            foreach(Alarm alarm in tag.Alarms) { 
                if(alarm.Type == AlarmType.LOW)
                {
                    if(currentValue < alarm.Border && checkIfAlarmDisplayRecently(alarm))
                    {
                        DateTime dateTime = DateTime.Now;
                        alarmList.Add(new AlarmValue
                        {
                            Id = alarm.Id,
                            Type = alarm.Type,
                            DateTime = dateTime,
                            Priority = alarm.Priority
                        });
                        writeAlarmsLog("type:" + alarm.Type + "; tagName:" + tag.TagName + "; priority: " + alarm.Priority + "; dateTime:" + dateTime.ToString() + "; border:" + alarm.Border);

                        string alarmMessage = "==========ALARM==========\nTYPE -> LOW; TAG NAME -> " + tag.TagName + "; UNIT-> " + tag.Units + "; CURRENT VALUE -> " + currentValue +  " IS UNDER ->" + alarm.Border;
                        writeAlarmMessageByPriority(alarm.Priority, alarmMessage);
                    }
                }else if(alarm.Type == AlarmType.HIGH)
                {
                    if(currentValue > alarm.Border && checkIfAlarmDisplayRecently(alarm))
                    {
                        DateTime dateTime = DateTime.Now;
                        alarmList.Add(new AlarmValue
                        {
                            Id = alarm.Id,
                            Type = alarm.Type,
                            DateTime = dateTime,
                            Priority = alarm.Priority
                        });
                        writeAlarmsLog("type:" + alarm.Type + "; tagName:" + tag.TagName + "; priority: " + alarm.Priority + "; dateTime:" + dateTime.ToString() + "; border:" + alarm.Border);

                        string alarmMessage = "==========ALARM==========\nTYPE -> HIGH; TAG NAME -> " + tag.TagName + "; UNIT-> " + tag.Units + "; CURRENT VALUE -> " + currentValue +  " IS ABOVE ->" + alarm.Border;
                        writeAlarmMessageByPriority(alarm.Priority, alarmMessage);
                    }
                }
            }
        }
        private bool checkIfAlarmDisplayRecently(Alarm alarm)
        {
            foreach (var alarmDisplay in alarmList)
            {
                if (alarm.Id == alarmDisplay.Id  && has15SecondsPassed(alarmDisplay.DateTime))
                {
                    return false;
                }
            }
            return true;
        }
        private void writeAlarmMessageByPriority(int priority, string alarmMessage)
        {
            for(int i = 0; i < priority; i++)
            {
                Write(callbacksAlarm, alarmMessage);
            }
        }
        
        private double GetFunction(string address) 
        {
            switch (address)
            {
                case "S":
                    return SimulationDriver.ReturnValue(address);
                case "C":
                    return SimulationDriver.ReturnValue(address);
                case "R":
                    return SimulationDriver.ReturnValue(address);
                default:
                    RealTimeDriver rtd = new RealTimeDriver();
                    return rtd.GetValue(address);
                    
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

        public void InitSubTrending()
        {
            callbacksTrending.Add(OperationContext.Current.GetCallbackChannel<ICallback>());
        }
        public void InitSubAlarm()
        {
            callbacksAlarm.Add(OperationContext.Current.GetCallbackChannel<ICallback>());
        }
        public static bool has15SecondsPassed(DateTime eventTime)
        {
            DateTime currentTime = DateTime.Now;

            TimeSpan timeDifference = currentTime - eventTime;

            return timeDifference.TotalSeconds < 15;
        }

    }
}
