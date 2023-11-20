using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class LOV_type_master
    {
        [Key]       
        public int reference_id { get; set; }
        
        [DataType("character varying")]
        public string? key { get; set; }
        [DataType("character varying")]
        public string? value { get; set; }
        [DataType("character varying")]
        public string? LOV_type { get; set; }
        
    }
}
