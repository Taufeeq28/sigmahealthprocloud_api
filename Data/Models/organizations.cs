using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("Organizations")]
    public class Organizations : Baseclass
    {
        [Key]
        public int organization_id { get; set; }
        [DataType("character varying")]
        public string? organization_name { get; set; }
    }
}
