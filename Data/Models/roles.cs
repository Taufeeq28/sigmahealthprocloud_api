using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class roles : baseclass
    {
        [Key]
        public int role_id { get; set; }

        [DataType("character varying")]
        public string role_name { get; set; }

    }
}
