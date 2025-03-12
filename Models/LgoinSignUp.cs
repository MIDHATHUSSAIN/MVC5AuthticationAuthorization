using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace webAPIandMVC.Models
{    

    [Table("LOGINSIGNUP")]
    public class LgoinSignUp
    {
        [Key]
        public int ID { get; set; }

        public string password { get; set; }

        public string email { get; set; }
    }
}