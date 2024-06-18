using CoreService.Interface;
using CoreService.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
            using(var db = new UserContextDB())
            {
                User user = new User(username,password);
                db.Users.Add(user);
                db.SaveChanges();
                foreach (var item in db.Users)
                {
                    Console.WriteLine(item.Username);
                }
            }
        }

        
    }
}
