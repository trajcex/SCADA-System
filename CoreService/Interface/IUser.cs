using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Interface
{
    [ServiceContract]
    internal interface IUser
    { 
        [OperationContract]
        void RegisterUser(string username, string password);
        
    }
}
