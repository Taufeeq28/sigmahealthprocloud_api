using Data.Models;


namespace BAL.Responses
{
    public class PatientDetailsResponse
    {
        public Guid Id { get; set; }
        public DateTime? DateOfHistoryVaccine { get; set; }
        public string? PatientStatus { get; set; }
        public Guid? CityId { get; set; }
        public Guid? StateId { get; set; }
        public Guid? CountryId { get; set; }

        //Person
        public string? PersonType { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? DateOfBirth { get; set; }
        public string? MiddleName { get; set; }
        public string? MotherFirstName { get; set; }
        public string? MotherLastName { get; set; }
        public string? MotherMaidenLastName { get; set; }

        public static PatientDetailsResponse FromPatientEntity(Patient patient, Person person)
        {
            return new PatientDetailsResponse
            {
                Id = patient.Id,
                DateOfHistoryVaccine = patient.DateOfHistoryVaccine,
                PatientStatus = patient.PatientStatus,
                CityId = patient.CityId,
                StateId = patient.StateId,
                CountryId = patient.CountryId,
                PersonType = person.PersonType,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Gender = person.Gender,
                DateOfBirth = person.DateOfBirth,
                MiddleName = person.MiddleName,
                MotherFirstName = person.MotherFirstName,
                MotherLastName = person.MotherLastName,
                MotherMaidenLastName = person.MotherMaidenLastName,
            };
        }
    }
}
