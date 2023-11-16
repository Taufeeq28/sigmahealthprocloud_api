using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class user_contact_info : baseclass
    {
        [Key]
        public int id { get; set; }
        [DataType("character varying")]      
        public string email { get; set; }
        public int phonenumber { get; set; }
        [DataType("character varying")]
        public string address { get; set; }
        [ForeignKey("fk_user_id")]
        public int user_id { get; set; }
        public virtual users users { get; set; }


    }
}
