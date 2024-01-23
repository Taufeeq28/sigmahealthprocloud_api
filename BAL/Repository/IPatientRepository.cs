using BAL.Constant;
using BAL.RequestModels;

namespace BAL.Repository
{
    public interface IPatientRepository : IGenericRepository<PatientModel>
    {
        public Task<PaginationModel<PatientModel>> GetAllAsync(SearchPatientParams search);
    }
}
