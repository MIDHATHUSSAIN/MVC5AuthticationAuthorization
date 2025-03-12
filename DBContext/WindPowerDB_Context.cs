using System.Data.Entity;
using webAPIandMVC.Models;

namespace webAPIandMVC.DBContext
{
    public class WindPowerDB_Context : DbContext
    {
        public WindPowerDB_Context() : base("graph") {

            //Database.SetInitializer<WindPowerDB_Context>(null);
        }
     
        public DbSet<WindModel> WindPowerDatas { get; set; }
    }
}
