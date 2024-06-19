using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealTimeUnit
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string uid = Guid.NewGuid().ToString();
            (int lowLimit, int highLimit) = InputValidator.GetValidatedInteger();

            Random random = new Random();   
            while (true)
            {
                int value =  random.Next(lowLimit,highLimit);
                Thread.Sleep(3000);
            }
          

        }
    }
}
