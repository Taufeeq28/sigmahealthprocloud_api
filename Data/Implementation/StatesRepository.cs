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
    public class StatesRepository : GenericRepository<States>, IStatesRepository
    {
        public StatesRepository(AppDbContext context) : base(context)
        {

        }
    }
}
