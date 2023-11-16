using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("states")]
    public class states : baseclass
    {
        [Key]
        public int state_id { get; set; }
        [DataType("character varying")]
        public string? state_name { get; set; }
        public int state_code { get; set; }
        public int zipcode { get; set; }
        [ForeignKey("fk_country_id")]
        public int fk_country_id { get; set; }
        public virtual countries country { get; set; }
    }
}
