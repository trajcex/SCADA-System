using CoreService.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoreService.Repository
{
    public class UserContextDB:DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}