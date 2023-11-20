using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("counties")]
    public class Counties : Baseclass
    {
        [Key]
        public int county_id { get; set; }
        [DataType("character varying")]
        public string? county_name { get; set; }

        public int county_code { get; set; }

        [ForeignKey("State")]
        public int state_id { get; set; }

        public virtual States? State { get; set; }


    }

}
