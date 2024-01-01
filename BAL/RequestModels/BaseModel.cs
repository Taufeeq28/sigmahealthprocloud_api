using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.RequestModels
{
    public class BaseModel
    {
        public Guid Id { get; set; }
        public DateTime? CreatedDate { get; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; } = DateTime.UtcNow;

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        public bool Isdelete { get; } = false;
    }
}
