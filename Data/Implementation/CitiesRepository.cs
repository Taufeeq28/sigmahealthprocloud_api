using Data;
using Data.Models;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class CitiesRepository : GenericRepository<City>, ICitiesRepository
    {
        public CitiesRepository(SigmaproIisContext context) : base(context)
        {

        }
    }
}
