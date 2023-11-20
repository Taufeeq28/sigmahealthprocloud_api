using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("States")]
    public class States : Baseclass
    {
        [Key]
        public int state_id { get; set; }
        [DataType("character varying")]
        public string? state_name { get; set; }
        public int state_code { get; set; }
        
        [ForeignKey("Countries")]
        public int country_id { get; set; }
        public virtual Countries? Countries { get; set; }
    }
}
