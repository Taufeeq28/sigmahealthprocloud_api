using BAL.RequestModels;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repository
{
    public interface ILOVTypeMasterRepository : IGenericRepository<LovMaster>
    {
        public Task<List<LOVMasterModel>> GetLOVMasterbyLOVTypeid(string lovtype);
    }
}
