using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webAPIandMVC.Models;
using System.Data.Entity;
namespace webAPIandMVC.DBContext
{
    public class LoginSignUpDB_Context : DbContext
    {
        public LoginSignUpDB_Context() : base("graph")
        {
            Database.SetInitializer<WindPowerDB_Context>(null);
        }

        public DbSet<LgoinSignUp> userData { get; set; }
    }
}