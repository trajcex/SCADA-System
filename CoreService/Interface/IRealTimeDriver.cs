using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CoreService.Interface
{
    [ServiceContract]
    public interface IRealTimeDriver
    {
        [OperationContract]
        (bool Check,int value) SendRandomValue(string address, int random, byte[] message );


        [OperationContract]
        bool ConnectRtu(string address);
    }


}
