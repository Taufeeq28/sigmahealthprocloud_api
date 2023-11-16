using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("facilities")]
    public class facilities : baseclass
    {
        [Key]
        public int facility_id { get; set; }
        [DataType("character varying")]
        public string? facility_name { get; set; }
        [ForeignKey("fk_jur_ord_id")]
        public int jur_ord_id { get; set; }
        public virtual Juridictions_organization_association jurd_org_ass { get; set; }

    }
}
