using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CoreService.Interface
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IReportManager" in both code and config file together.
    [ServiceContract]
    public interface IReportManager
    {
        [OperationContract]
        string GetLastValueAi();
        [OperationContract]
        string GetLastValueDi();
        [OperationContract]
        string GetSortedValuesByTagName(string tagName);
        [OperationContract]
        List<string> GetAllTagNames();
        [OperationContract]
        string GetTagValuesInPeriod(DateTime startTime, DateTime endTime);
        [OperationContract]
        string GetAlarmsInPeriod(DateTime startTime, DateTime endTime);
        [OperationContract]
        string GetAlarmsByPriority(int priority);

        [OperationContract]
        List<string> GetAllPriorities();
    }
}
