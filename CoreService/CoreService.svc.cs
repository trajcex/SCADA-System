using CoreService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CoreService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CoreService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CoreService.svc or CoreService.svc.cs at the Solution Explorer and start debugging.
    public class CoreService : ICore,IUser
    {
        public void DoWork()
        {
        }

        public void RegisterUser(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
