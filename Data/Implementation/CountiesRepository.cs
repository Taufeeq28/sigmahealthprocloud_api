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
    public class CountiesRepository : GenericRepository<counties>, ICountiesRepository
    {
        public CountiesRepository(AppDbContext context) : base(context)
        {

        }
    }
}
