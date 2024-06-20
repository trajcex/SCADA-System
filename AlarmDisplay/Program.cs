using AlarmDisplay.TagProcessingReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AlarmDisplay
{
    public class Program
    {
        public class Callback : IAlarmMonitoringCallback
        {
            public void Message(string message)
            {
                Console.WriteLine(message);
            }
        }
        static AlarmMonitoringClient alarmMonitoringClient;
        static void Main(string[] args)
        {
            InstanceContext ic = new InstanceContext(new Callback());
            alarmMonitoringClient = new AlarmMonitoringClient(ic);
            alarmMonitoringClient.InitSubAlarm();
            Console.ReadKey();
        }
    }
}
