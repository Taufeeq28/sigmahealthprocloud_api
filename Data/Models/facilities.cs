using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    [Table("Facilities")]
    public class Facilities : Baseclass
    {
        [Key]       
        public int facility_id { get; set; }
        [DataType("character varying")]
        public string? facility_name { get; set; }
        [ForeignKey("jurd_org_ass")]
        public int juridictions_organization_id { get; set; }
        public virtual Juridictions_organization_association? jurd_org_ass { get; set; }
        
        [DataType("character varying")]
        public string? administered_at_location { get; set; }
        [DataType("character varying")]
        public string? sending_organization { get; set; }
        [DataType("character varying")]
        public string? responsible_organization { get; set; }
    }
}
