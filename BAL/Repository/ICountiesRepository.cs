using BAL.RequestModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repository
{
    public interface ICountiesRepository : IGenericRepository<County>
    {
        public Task<List<CountyModel>> GetCountybyStateid(Guid stateid);
    }
}
