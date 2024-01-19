using BAL.Pagination;
using BAL.Repository;
using BAL.RequestModels;
using BAL.Constant;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using SQLitePCL;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.PortableExecutable;

namespace BAL.Implementation
{
    public class PatientRepository : IGenericRepository<PatientModel>, IPatientRepository
    {
        private SigmaproIisContext context;
        private ILogger<UnitOfWork> _logger;
        private readonly string _corelationId = string.Empty;
        public PatientRepository(SigmaproIisContext _context, ILogger<UnitOfWork> logger)
        {
            context = _context;
            _logger = logger;
        }
        public async Task<IEnumerable<PatientModel>> Find(Expression<Func<PatientModel, bool>> predicate)
        {
            return (IEnumerable<PatientModel>)await context.Set<PatientModel>().FindAsync(predicate);
        }
        public async Task<IEnumerable<PatientModel>> GetAllAsync()
        {
            return await context.Set<PatientModel>().ToListAsync();
        }
        public async Task<IEnumerable<PatientModel>> GetAllAsync(SearchPatientParams search)
        {
            var patientModelList = new List<PatientModel>();
            string keyword = search.keyword.IsNullOrEmpty() ? string.Empty : search.keyword.Trim().ToLower();
            string cityname = search.city.IsNullOrEmpty() ? string.Empty : search.city.Trim().ToLower();
            string statename = search.state.IsNullOrEmpty() ? string.Empty : search.state.Trim().ToLower();
            string dateofhistoryvaccine = search.date_of_history_vaccine.IsNullOrEmpty() ? string.Empty : search.date_of_history_vaccine.Trim().ToLower();
            string patientstatus = search.patient_status.IsNullOrEmpty() ? string.Empty : search.patient_status.Trim().ToLower();
            string patientname = search.patient_name.IsNullOrEmpty() ? string.Empty : search.patient_name.Trim().ToLower();
            string patientcountry = search.country.IsNullOrEmpty() ? string.Empty : search.country.Trim().ToLower();
            //if (keyword.IsNullOrEmpty() && dateofhistoryvaccine.IsNullOrEmpty() && cityname.IsNullOrEmpty() && statename.IsNullOrEmpty() && patientstatus.IsNullOrEmpty() && patientcountry.IsNullOrEmpty() && patientname.IsNullOrEmpty())
            //{
            //    return patientModelList;
            //}

            var patientList = await context.Patients.

                Join(context.People, pt => pt.PersonId.Value.ToString(), ft => ft.Id.ToString(), (pt, ft) => new { patients = pt, person = ft }).

                Join(context.Cities, pt => pt.patients.CityId, ct => ct.Id, (pt, ct) => new { pt.person, pt.patients, cities = ct }).

                Join(context.States, pt => pt.patients.StateId, st => st.Id, (ct, st) => new { ct.person, ct.patients, states = st, ct.cities }).

                 //Join(context.Counties, pt => pt.patients.CountryId, ct => ct.Id, (cr, ct) => new { cr.person, cr.patients, cr.states, countries = ct, cr.cities }).
                 Where(i => i.person.FirstName.ToLower().Contains(keyword)).
                Select(i => new
                {
                    i.patients.Id,
                    i.person.FirstName,
                    i.person.LastName,
                    i.patients.PersonId,
                    i.patients.DateOfHistoryVaccine,
                    i.patients.PatientStatus,
                    //i.countries.CountyName,
                    i.cities.CityName,
                    i.cities.State.StateName
                }).ToPagedListAsync(search.pagenumber, search.pagesize);


            Parallel.ForEach(patientList, async i =>
            {
                var model = new PatientModel()
                {   
                    Id = i.Id,
                    FirstName = i.FirstName,
                    PersonId = i.PersonId,
                    DateOfHistoryVaccine1 = i.DateOfHistoryVaccine,
                    PatientStatus = i.PatientStatus,
                    //Country = i.CountyName,
                    State = i.StateName,
                    City = i.CityName

                };
                patientModelList.Add(model);
            });
            Task.WhenAll();
            return patientModelList;
        }

        public async Task<PatientModel> GetByIdAsync(int id)
        {
            return await context.Set<PatientModel>().FindAsync(id);
        }

        public async Task<ApiResponse<string>> InsertAsync(PatientModel model)
        {
            try
            {
                var newperson = new Person()
                {
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    Gender = model.Gender,                  
                    MotherFirstName=model.MotherFirstName,
                    MotherMaidenLastName=model.MotherMaidenLastName,
                    MotherLastName=model.MotherLastName,
                    PersonType=model.PersonType,
                    DateOfBirth=model.DateOfBirth.ToString(),                  
                    
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    UpdatedBy = model.UpdatedBy,
                    UpdatedDate = model.UpdatedDate,
                    Isdelete = model.Isdelete,
                };
                context.People.Add(newperson);
                await context.SaveChangesAsync();

                TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");

                var newpatient = new Patient()
                {
                    DateOfHistoryVaccine = TimeZoneInfo.ConvertTimeToUtc(model.DateOfHistoryVaccine, tzi),
                    
                    PatientStatus = model.PatientStatus,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    UpdatedBy = model.UpdatedBy,
                    UpdatedDate = model.UpdatedDate,
                    Isdelete = model.Isdelete,
                    PersonId = newperson.Id,
                    CityId = new Guid(model.City),
                    StateId = new Guid(model.State),
                    CountryId = new Guid(model.Country)

                };
                context.Patients.Add(newpatient);
                await context.SaveChangesAsync();
                return ApiResponse<string>.Success(newpatient.Id.ToString(), "Patient created successfully.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(InsertAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while creating the site.");
            }
        }

        public async Task<ApiResponse<string>> UpdateAsync(PatientModel model)
        {
            if (model == null)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Invalid input. EditSiteRequest object is null in Method: {nameof(UpdateAsync)}");
                return ApiResponse<string>.Fail("Invalid input. EditSiteRequest object is null.");
            }
            try
            {
                if (model != null)
                {
                    var updatePatient = new Patient()
                    {
                        PatientId = model.PatientId,
                        CreatedBy = model.CreatedBy,
                        CreatedDate = model.CreatedDate,
                        UpdatedBy = model.UpdatedBy,
                        UpdatedDate = model.UpdatedDate,
                        Isdelete = model.Isdelete,
                    };
                    context.Patients.Update(updatePatient);
                    await context.SaveChangesAsync();

                    return ApiResponse<string>.Success(updatePatient.Id.ToString(), "Patient record updated successfully.");
                }
                return ApiResponse<string>.Fail("Patient with the given ID not found.");

            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(UpdateAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("An error occurred while updating the patient.");
            }
        }
        public async Task<ApiResponse<string>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await context.Set<Patient>().FindAsync(id);
                if (entity != null)
                {
                    entity.Isdelete = true;
                    context.Patients.Update(entity);
                    await context.SaveChangesAsync();
                    return ApiResponse<string>.Success(id.ToString(), "Patient deleted successfully.");
                }

                return ApiResponse<string>.Fail("Patient with the given ID not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"CorelationId: {_corelationId} - Exception occurred in Method: {nameof(DeleteAsync)} Error: {exp?.Message}, Stack trace: {exp?.StackTrace}");
                return ApiResponse<string>.Fail("Patient with the given ID not found.");
            }
        }

           }
}
