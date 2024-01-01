using BAL.RequestModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repository
{
    public interface ICitiesRepository : IGenericRepository<City>
    {
        public Task<List<CityModel>> GetCitybyStateid(Guid stateid);
        public Task<List<CityModel>> GetCitybyStateidandCountyid(Guid stateid, Guid countyid);
    }
}
