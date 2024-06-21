using RealTimeUnit.RTDServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealTimeUnit
{

    public class Program
    {
        static RealTimeDriverClient driverClient = new RealTimeDriverClient();
        static void Main(string[] args)
        {
            Sender sender = new Sender();
            sender.CreateAsmKeys();
            string address;
            while (true)
            {
                address = InputValidator.GetValidAddress();
                bool check = driverClient.ConnectRtu(address);
                if (check) { 
                    break;
                }
            }
            sender.ExportPublicKey(address);
            (int lowLimit, int highLimit) = InputValidator.GetValidatedInteger();
            while (true)
            {
                int value = RandomGenerator.GenerateRandomNumber(lowLimit, highLimit);
                var signature = sender.SignMessage(value.ToString());
                var ret = driverClient.SendRandomValue(address, value,signature);
                if (ret.Item1)
                {
                    Console.WriteLine("The message is accepted. Post value is " + ret.Item2.ToString());
                }
                else
                {
                    Console.WriteLine("The message is not accepted. Last value is " + ret.Item2.ToString());
                }
                Thread.Sleep(3000);
            }

        } 
    }
}
