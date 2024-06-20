﻿using System;
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
        void SendRandomValue(string uid, int random);


        [OperationContract]
        bool ConnectRtu(string uid);
    }


}
