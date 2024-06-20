using SharedLibrary.Model;
using System;
using MySql.Data.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoreService
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class UserContextDB:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TagValue> TagValues { get; set; }
        public UserContextDB() : base("name=DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            base.OnModelCreating(modelBuilder);
        }
    }

}