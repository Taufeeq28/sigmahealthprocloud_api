using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class user_types : baseclass
    {
        [Key]
        public int user_type_id { get; set; }

        [DataType("character varying")]
        public string user_types_name { get; set; }

    }
}
