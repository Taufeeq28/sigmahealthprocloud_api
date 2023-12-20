using Data.Constant;
using Data.Models;
using Data.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IOrganizationsRepository : IGenericRepository<OrganizationModel>
    {
        public Task<List<OrganizationModel>> GetOrganizationByJuridictionId(Guid jurdid);
        
    }
}
