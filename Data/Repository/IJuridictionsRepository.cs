using Data.Models;
using Data.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IJuridictionsRepository : IGenericRepository<Juridiction>
    {
        public Task<List<JuridictionModel>> GetJuridictionsbyBusinessid(Guid businessid);
    }
}
