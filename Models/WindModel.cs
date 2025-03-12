using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webAPIandMVC.Models
{
    //[Table("GRAPHTABLE")]
    public class WindModel
    {
        [Key]
        public int ID { get; set; }
        public int POWERR { get; set; } 
        public int WIND { get; set; }
    }
}