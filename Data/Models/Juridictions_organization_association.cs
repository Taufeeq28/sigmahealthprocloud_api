using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Juridictions_organization_association:baseclass
    {
        [Key]
        public int jurd_org_id { get; set; }
        [ForeignKey("fk_org_id")]
        public int organization_id { get; set; }

        [ForeignKey("fk_jurd_id")]
        public int jurisdiction_id { get; set; }

        public virtual organizations organizations { get; set; }
        public virtual jurisdictions jurisdictions { get; set; }

    }
}
