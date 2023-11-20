using Data.Context;
using Data.Models;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class LovTypeMasterRepository : GenericRepository<LOV_type_master>, ILOVTypeMasterRepository
    {
        public LovTypeMasterRepository(AppDbContext context) : base(context)
        {

        }
    }
  
}
