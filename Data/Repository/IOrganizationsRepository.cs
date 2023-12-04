using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IOrganizationsRepository : IGenericRepository<Organization>
    {
        public IEnumerable<Organization> GetOrganizationByJuridictionId(string jurdid);
    }
}
