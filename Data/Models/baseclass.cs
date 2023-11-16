using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class baseclass
    {
        [Column(Order = 1)]
        public Guid id { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? updated_date { get; set; }
        [DataType("character varying")]
        public string? createdby { get; set; }
        [DataType("character varying")]
        public string? updatedby { get; set; }
        public bool? isdelete { get; set; }
    }
}
