using DatabaseManager.CoreService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    public class Program
    {
        static UserClient service = new UserClient();
        static void Main(string[] args)
        {
            service.RegisterUser("nikola", "123");
        }
    }
}
