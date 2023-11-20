using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Juridictions_organization_association:Baseclass
    {
        [Key]
        public int juridictions_organization_id { get; set; }
        [ForeignKey("organizations")]
        public int organization_id { get; set; }

        [ForeignKey("jurisdictions")]
        public int jurisdiction_id { get; set; }

        public virtual Organizations? organizations { get; set; }
        public virtual Jurisdictions? jurisdictions { get; set; }

    }
}
