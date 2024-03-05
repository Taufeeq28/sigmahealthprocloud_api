using BAL.Repository;
using BAL.RequestModels;
using BAL.Constant;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using BAL.Responses;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using BAL.Interfaces;
using System.Reflection;

namespace BAL.Implementation
{
    public class PatientRepository : IGenericRepository<PatientModel>, IPatientRepository, IPatientService
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

        public async Task<PaginationModel<PatientModel>> GetAllAsync(SearchPatientParams search)
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

                Join(context.People, pt => pt.PersonId, ft => ft.Id, (pt, ft) => new { patients = pt, person = ft }).

                Join(context.Cities, pt => pt.patients.CityId, ct => ct.Id, (pt, ct) => new { pt.person, pt.patients, cities = ct }).

                Join(context.States, pt => pt.patients.StateId, st => st.Id, (ct, st) => new { ct.person, ct.patients, states = st, ct.cities }).

                 Join(context.Countries, pt => pt.patients.CountryId, ct => ct.Id, (cr, ct) => new { cr.person, cr.patients, cr.states, countries = ct, cr.cities })
                               
                .Where(i => i.person.FirstName.ToLower().Contains(keyword) && i.patients.Isdelete==false).
                Select(i => new
                {
                    i.patients.Id,
                    i.person.FirstName,
                    i.person.MiddleName,
                    i.person.LastName,
                     i.person.Gender,
                    

                    i.person.MotherFirstName,
                    i.person.MotherLastName,
                    i.person.MotherMaidenLastName,
                    i.patients.PersonId,
                    i.person.PersonType,
                    i.patients.DateOfHistoryVaccine,
                 //   i.patients.DateOfBirth,
                    i.patients.PatientStatus,
                    i.countries.CountryName,
                    countryid = i.countries.Id,
                    i.cities.CityName,
                    cityid = i.cities.Id,
                    i.states.StateName,
                    stateid = i.states.Id

                }).ToListAsync();
            foreach (var i in patientList)
            {
                var model = new PatientModel();
                var entityContact = context.Contacts.FirstOrDefault(x => x.EntityId == i.Id);
                var contactNumber = string.Empty;
                var email = string.Empty;
                var entityAddress = context.EntityAddresses.FirstOrDefault(x => x.EntityId == i.Id);

                var gendervalue = string.Empty;
                Guid pid = new Guid(i.Gender);
                var gender = context.LovMasters.FirstOrDefault(x => x.Id == pid);

                if (entityAddress != null)
                {
                    var aid = entityAddress.Addressid;
                    var addressData = context.Addresses.FirstOrDefault(x => x.Id == aid);
                    var CountryName = context.Countries.FirstOrDefault(x => x.Id == addressData.CountryId).CountryName;
                    var StateName = context.States.FirstOrDefault(x => x.Id == addressData.StateId).StateName;
                    var CityName = context.Cities.FirstOrDefault(x => x.Id == addressData.CityId).CityName;

                    model = new PatientModel()
                    {
                        Id = i.Id,
                        FirstName = i.FirstName,
                        LastName = i.LastName,
                        MiddleName = i.MiddleName,
                        GenderId=i.Gender,
                        Gender = gender.Value,
                        
                        MotherFirstName = i.MotherFirstName,
                        MotherLastName = i.MotherLastName,
                        MotherMaidenLastName = i.MotherMaidenLastName,
                        PersonType = i.PersonType,
                        PersonId = (Guid)i.PersonId,
                        DateOfHistoryVaccine1 = i.DateOfHistoryVaccine,
                      //  DateOfBirth=i.DateOfBirth,
                        PatientStatus = i.PatientStatus,
                        Country = i.CountryName,
                        CountryId = i.countryid,
                        State = i.StateName,
                        StateId = i.stateid,
                        City = i.CityName,
                        CityId = i.cityid,
                        Address = addressData.Line1 + " " + addressData.Line2 + " " + addressData.Suite + " " + CountryName + " " + StateName + " " + CityName + " " + addressData.ZipCode,
                        ContactValue = (entityContact == null) ? "" : entityContact.ContactValue,
                        ContactType = (entityContact == null) ? "" : entityContact.ContactType
                    };
                    patientModelList.Add(model);
                }

            }


            Task.WhenAll();

            long? totalRows = patientModelList.Count();
            var response = patientModelList.Skip(search.pagesize * (search.pagenumber - 1)).Take(search.pagesize).ToList();
            return PaginationHelper.Paginate(response, search.pagenumber, search.pagesize, Convert.ToInt32(totalRows));
        }

        public async Task<ApiResponse<PatientDetailsResponse>> GetPatientDetailsById(Guid patientId)
        {
            try
            {
                var patient = await context.Patients.FindAsync(patientId);
                if (patient != null)
                {
                    var person = await context.People.FindAsync(patient.PersonId);
                    var patientDetails = PatientDetailsResponse.FromPatientEntity(patient, person);

                    return ApiResponse<PatientDetailsResponse>.Success(patientDetails, "Patient details fetched successfully.");
                }

                _logger.LogError($"Patient with ID {patientId} not found.");
                return ApiResponse<PatientDetailsResponse>.Fail("Patient not found.");
            }
            catch (Exception exp)
            {
                _logger.LogError($"An error occurred: {exp.Message}, Stack trace: {exp.StackTrace}");
                return ApiResponse<PatientDetailsResponse>.Fail("An error occurred while fetching facility details.");
            }
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
                    MotherFirstName = model.MotherFirstName,
                    MotherMaidenLastName = model.MotherMaidenLastName,
                    MotherLastName = model.MotherLastName,
                    PersonType = model.PersonType,
                    DateOfBirth = model.DateOfBirth.ToString(),

                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    UpdatedBy = model.UpdatedBy,
                    UpdatedDate = model.UpdatedDate,
                    Isdelete = model.Isdelete,
                };
                if (!model.IsEdit)
                {
                    context.People.Add(newperson);
                }
                else
                {
                    newperson.Id = model.PersonId;
                    context.People.Update(newperson);
                }
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
                if (!model.IsEdit)
                {
                    context.Patients.Add(newpatient);
                }
                else
                {
                    newpatient.Id = model.Id;
                    context.Patients.Update(newpatient);
                }
                await context.SaveChangesAsync();

                var entityAddress = new EntityAddress()
                {
                    EntityType = model.EntityType,
                    AddressType = model.AddressType,
                    Addressid = new Guid(model.Address),
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    UpdatedBy = model.UpdatedBy,
                    UpdatedDate = model.UpdatedDate,
                    EntityId = newpatient.Id
                };

                context.EntityAddresses.Add(entityAddress);
                context.SaveChanges();
                Random on = new Random();
                var contact = new Contact()
                {
                    ContactsId = on.Next(2).ToString(),
                    EntityType = model.EntityType,
                    ContactValue = model.ContactValue,
                    ContactType = model.ContactType,
                    Isdelete = false,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    UpdatedBy = model.UpdatedBy,
                    UpdatedDate = model.UpdatedDate,
                    EntityId = newpatient.Id
                };
                context.Contacts.Add(contact);
                context.SaveChanges();

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

                    var personEntity = await context.Set<Person>().FindAsync(entity.PersonId);
                    personEntity.Isdelete = true;
                    context.People.Update(personEntity);

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

        public Task<PatientModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
