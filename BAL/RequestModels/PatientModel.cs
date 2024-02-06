using Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BAL.RequestModels
{
    public class PatientModel : BaseModel
    {
        [Required]
        public int PatientId { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? MiddleName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public DateTime? DateOfHistoryVaccine1 { get; set; }

        public DateTime DateOfHistoryVaccine { get; set; }

        [Required]
        public string? MotherFirstName { get; set; }

        [Required]
        public string? MotherMaidenLastName { get; set; }

        [Required]
        public string? MotherLastName { get; set; }


        [Required]
        public string? PatientStatus { get; set; }

        [Required]
        public string? PersonType { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public Guid? CityId { get; set; }

        [Required]
        public string? State { get; set; }

        [Required]
        public Guid? StateId { get; set; }

        [Required]
        public string? Country { get; set; }

        [Required]
        public Guid? CountryId { get; set; }

        [Required]
        public string? ZipCode { get; set; }

        [Required]
        public Guid? PersonId { get; set; }

        [JsonIgnore]
        public long? TotalRows { get; set; }
    }
}
