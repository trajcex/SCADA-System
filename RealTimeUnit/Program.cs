using RealTimeUnit.RTDServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
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
            string uid = Guid.NewGuid().ToString();
            bool check = driverClient.ConnectRtu(uid);
            (int lowLimit, int highLimit) = InputValidator.GetValidatedInteger();
            while (true)
            {
                int value = RandomGenerator.GenerateRandomNumber(lowLimit, highLimit);
                driverClient.SendRandomValue(uid, value);
                Thread.Sleep(3000);
            }

        } 
    }
}
