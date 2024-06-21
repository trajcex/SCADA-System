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
using System.Data.Entity;

namespace CoreService
{

    public class TagProcessing : ITagProcessing,IMonitoring, IAlarmMonitoring
    {
        
        private static List<ICallback> callbacksTrending = new List<ICallback>();
        private static List<ICallback> callbacksAlarm = new List<ICallback>();
        private static Dictionary<string, Thread > tagMap = new Dictionary<string, Thread>();
        private readonly object locker = new object();
        private static List<AlarmValue> alarmList = new List<AlarmValue>();
        public TagProcessing() { }

        public void StopTag(string tagName)
        {
            tagMap[tagName].Abort();
            //tagMap[tagName].Join();
            tagMap.Remove(tagName);
        }
        
        public void StartTag(Tag tag)
        {
  
            Thread thread = new Thread(() => ProcessTag(tag));
            tagMap[tag.TagName] = thread;
            thread.Start();
        }

        private void ProcessTag(Tag tag) {
            try
            {

                while (true)
                {
             
                    if (tag.GetType() == typeof(DigitalInputTag))
                    {
                        DigitalInputTag digitalInputTag = (DigitalInputTag) tag;
                        double value = GetFunction(digitalInputTag.Address);

                        if (value != 0 && value != 1) continue;

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

                        if (value > analogInputTag.HighLimit || value < analogInputTag.LowLimit) continue;
                        
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
            catch (ThreadAbortException ex)
            {
                Console.WriteLine($"");
            }
            catch (Exception)
            {

                Console.WriteLine();
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
                        lock (locker)
                        {
                            DateTime dateTime = DateTime.Now;
                            AlarmValue alarmValue = new AlarmValue
                            {
                                AlarmId = alarm.Id,
                                Type = alarm.Type,
                                DateTime = dateTime,
                                Priority = alarm.Priority
                            };
                            alarmList.Add(alarmValue);
                            writeAlarmsLog("type:" + alarm.Type + "; tagName:" + tag.TagName + "; priority: " + alarm.Priority + "; dateTime:" + dateTime.ToString() + "; border:" + alarm.Border);

                            SaveAlarmValue(alarmValue);

                            string alarmMessage = "==========ALARM==========\nTYPE -> LOW; TAG NAME -> " + tag.TagName + "; UNIT-> " + tag.Units + "; CURRENT VALUE -> " + currentValue + " IS UNDER ->" + alarm.Border;
                            writeAlarmMessageByPriority(alarm.Priority, alarmMessage);
                        }
                    }
                }else if(alarm.Type == AlarmType.HIGH)
                {
                    if(currentValue > alarm.Border && checkIfAlarmDisplayRecently(alarm))
                    {
                        lock (locker)
                        {
                            DateTime dateTime = DateTime.Now;
                            AlarmValue alarmValue = new AlarmValue
                            {
                                AlarmId = alarm.Id,
                                Type = alarm.Type,
                                DateTime = dateTime,
                                Priority = alarm.Priority
                            };
                            alarmList.Add(alarmValue);

                            SaveAlarmValue(alarmValue);

                            writeAlarmsLog("type:" + alarm.Type + "; tagName:" + tag.TagName + "; priority: " + alarm.Priority + "; dateTime:" + dateTime.ToString() + "; border:" + alarm.Border);

                            string alarmMessage = "==========ALARM==========\nTYPE -> HIGH; TAG NAME -> " + tag.TagName + "; UNIT-> " + tag.Units + "; CURRENT VALUE -> " + currentValue + " IS ABOVE ->" + alarm.Border;
                            writeAlarmMessageByPriority(alarm.Priority, alarmMessage);
                        }
                        
                    }
                }
            }
        }
        private bool checkIfAlarmDisplayRecently(Alarm alarm)
        {
            foreach (var alarmDisplay in alarmList)
            {
                if (alarm.Id == alarmDisplay.AlarmId  && has15SecondsPassed(alarmDisplay.DateTime))
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
                using (var dbContext = new UserContextDB()) 
                {
                    dbContext.TagValues.Add(tagValue);
                    dbContext.SaveChanges();
                }

            }
        }
        private void SaveAlarmValue(AlarmValue alarmValue)
        {
            lock (locker)
            {
                using (var dbContext = new UserContextDB())
                {
                    dbContext.Alarms.Add(alarmValue);
                    dbContext.SaveChanges();
                }

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
