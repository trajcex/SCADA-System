using SharedLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace CoreService.Interface
{
    [ServiceContract]
    public interface ITagProcessing
    {
       
        [OperationContract]
        void StartTag(string tagName);
        [OperationContract]
        void StopTag(string tagName);
    }

    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface IMonitoring
    {
        [OperationContract(IsOneWay = true)]
        void InitSubTrending();
    }
    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface IAlarmMonitoring
    {
        [OperationContract(IsOneWay = true)]
        void InitSubAlarm();
    }
    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void Message(string message);
    }

}
