using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("jurisdictions")]
    public class jurisdictions : baseclass
    {
        [Key]
        public int jurisdiction_id { get; set; }
        [DataType("character varying")]
        public string? jurisdiction_name { get; set; }
        public int zipcode { get; set; }
    }
}
