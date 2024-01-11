using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Request
{
    public class CreateEntityContactsRequest
    {
        [Required]
        public string ContactValue { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public bool Isdelete { get; set; }

        [Required]
        public string ContactType { get; set; }

        [Required]
        public Guid EntityId { get; set; }

        [Required]
        public string EntityType { get; set; }
    }
}
