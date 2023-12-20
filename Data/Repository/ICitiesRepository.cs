using Data.Models;
using Data.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface ICitiesRepository : IGenericRepository<City>
    {
        public Task<List<CityModel>> GetCitybyStateid(Guid stateid);
        public Task<List<CityModel>> GetCitybyStateidandCountyid(Guid stateid,Guid countyid);
    }
}
