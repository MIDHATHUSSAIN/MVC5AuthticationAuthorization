using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webAPIandMVC.Models
{
    [Table("Employees")]
    public class data
    {
        [Key]
        
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string Department { get; set; }
        public string CreatedAt { get; set; }
       
    }
}