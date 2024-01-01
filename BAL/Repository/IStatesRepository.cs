using BAL.RequestModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repository
{
    public interface IStatesRepository : IGenericRepository<State>
    {
        public Task<List<StateModel>> GetAllStates();
        public Task<List<StateModel>> GetStatebyCountryid(Guid countryid);
    }
}
