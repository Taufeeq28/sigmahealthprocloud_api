using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Contacts_Type:Baseclass
    {
        [Key]
        public int contact_type_id { get; set; }

        [DataType("character varying")]
        public string? email { get; set; }
        [DataType("character varying")]
        public string? phone { get; set; }
        [DataType("character varying")]
        public string? cell { get; set; }
    }
}
