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
    public class FacilitiesRepository : GenericRepository<facilities>, IFacilitiesRepository
    {
        public FacilitiesRepository(AppDbContext context) : base(context)
        {

        }
    }
}
