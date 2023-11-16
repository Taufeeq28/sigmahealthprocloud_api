using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("cities")]
    public class cities : baseclass
    {
        [Key]
        public int city_id { get; set; }
        [DataType("character varying")]
        public string? city_name { get; set; }
        public int? zipcode { get; set; }

        [ForeignKey("fk_county_id")]
        public int county_id { get; set; }
       
        [ForeignKey("fk_state_id")]
        public int state_id { get; set; }
        public virtual states states { get; set; }
        public virtual counties Counties { get; set; }

    }
}
