using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Addresses:Baseclass
    {
        [Key]
        public int address_id { get; set; }

        [DataType("character varying")]
        public string? line1 { get; set; }
        [DataType("character varying")]
        public string? line2 { get; set; }
        [DataType("character varying")]
        public string? Suite { get; set; }
      
        [ForeignKey("Counties")]
        public int county_id { get; set; }
        [ForeignKey("Countries")]
        public int country_id { get; set; }

        [ForeignKey("States")]
        public int state_id { get; set; }
        public virtual States? States { get; set; }
        public virtual Counties? Counties { get; set; }
        public virtual Countries? Countries { get; set; }


    }
}
