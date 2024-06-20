using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Trending.TagProcessingServiceReference;

namespace Trending
{
    internal class Program
    {
        public class Callback : IMonitoringCallback
        {
            public void Message(string message)
            {
                Console.WriteLine(message);
            }
        }
        static MonitoringClient monitroing;
        static void Main(string[] args)
        {
            InstanceContext ic = new InstanceContext(new Callback());
            monitroing = new MonitoringClient(ic);
            monitroing.InitSub();
            Console.ReadKey();
        }
    }
}
