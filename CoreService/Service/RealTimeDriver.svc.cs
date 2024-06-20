using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using CoreService.Interface;

namespace CoreService
{
    public class RealTimeDriver : IRealTimeDriver
    {
        private static Dictionary<string ,int > rtuDicDictionary = new Dictionary<string ,int>();

        public bool ConnectRtu(string uid)
        {
            if (rtuDicDictionary.ContainsKey(uid))
            {
                return false;
            }
            else
            {
                rtuDicDictionary.Add(uid, 0);
                return true;
            }
        }

        public void SendRandomValue(string uid, int random)
        {
            rtuDicDictionary[uid] = random;
        }

  
    }
}
