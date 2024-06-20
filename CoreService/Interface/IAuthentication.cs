using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CoreService.Interface
{
    [ServiceContract]
    internal interface IAuthentication
    {
        [OperationContract]
        bool Registration(string username, string password);
        [OperationContract]
        string Login(string username, string password);
        [OperationContract]
        bool Logout(string token);

        [OperationContract]
        bool IsUserAuthenticated(string token);
    }
}
