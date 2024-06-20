using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CoreService
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
    }
}
