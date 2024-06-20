using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeUnit
{
    public static class RandomGenerator
    {
        public static int GenerateRandomNumber( int min, int max)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            long range = (long)max - (long)min;
            byte[] randomNumber = new byte[4];
            rng.GetBytes(randomNumber);
            int rand = BitConverter.ToInt32(randomNumber, 0) & 0x7FFFFFFF;
            return (int)((rand % range) + min);
        }
    }
}
