using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CoreService.Interface
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICoreService" in both code and config file together.
    [ServiceContract]
    public interface ICore
    {
        [OperationContract]
        void DoWork();
    }


}
