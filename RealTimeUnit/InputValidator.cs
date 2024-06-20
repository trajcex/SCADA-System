using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeUnit
{
    internal static class InputValidator
    {
        public static (int,int) GetValidatedInteger()
        {
            int lowLimit;
            int highLimit;

            while (true)
            {
                Console.WriteLine("Please enter the lower limit (lowLimit):");

                string input = Console.ReadLine();

                if (int.TryParse(input, out lowLimit))
                {
                    Console.WriteLine($"You entered a valid number for lowLimit: {lowLimit}");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

            while (true)
            {
                Console.WriteLine("Please enter the upper limit (highLimit):");

                string input = Console.ReadLine();

                if (int.TryParse(input, out highLimit))
                {
                    if (highLimit > lowLimit)
                    {
                        Console.WriteLine($"You entered a valid number for highLimit: {highLimit}");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("highLimit must be greater than lowLimit. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

            return (lowLimit, highLimit);
        }

    }
}
