using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Users : Baseclass
    {
        [Key]
        public int users_id { get; set; }
        [DataType("character varying")]
        public string? user_firstname { get; set; }
        [DataType("character varying")]
        public string? user_lasttname { get; set; }
        [DataType("character varying")]
        public string? designation { get; set; }
       
        [ForeignKey("user_types")]
        public int user_type_id { get; set; }
        public virtual User_Types? user_types { get; set; }
      
    }
}
