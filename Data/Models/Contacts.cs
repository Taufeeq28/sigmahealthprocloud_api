using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Contacts : Baseclass
    {
        [Key]
        public int contact_id { get; set; }
        [ForeignKey("Contacts_Type")]
        public int contact_type_id { get; set; }
       
        [DataType("character varying")]
        public string? contact_value { get; set; }
       
        public virtual Contacts_Type? Contacts_Type { get; set; }


    }
}
