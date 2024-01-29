using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAL.Request
{
    public class CreateMasterAddressRequest
    {
        [Required]
        public string? Line1 { get; set; }

        [Required]
        public string? Line2 { get; set; }

        [DefaultValue(null)]
        public string? Suite { get; set; }
        
        [Required]
        public string? CreatedBy { get; set; }
        
        [Required]
        public string? CountyName { get; set; }

        [Required]
        public string? CountryName { get; set; }

        [Required]
        public string? StateName { get; set; }

        [Required]
        public string? CityName { get; set; }

        [Required]
        public string? ZipCode { get; set; }
    }
    public interface IEntity
    {
        Guid GetId();
        string GetName();
    }

    public class CountrySearch : IEntity
    {
        public Guid Id { get; set; }
        public string CountryName { get; set; }

        public Guid GetId() => Id;
        public string GetName() => CountryName;
    }

    public class StateSearch : IEntity
    {
        public Guid Id { get; set; }
        public string StateName { get; set; }

        public Guid GetId() => Id;
        public string GetName() => StateName;
    }

    public class CountySearch : IEntity
    {
        public Guid Id { get; set; }
        public string CountyName { get; set; }

        public Guid GetId() => Id;
        public string GetName() => CountyName;
    }

    public class CitySearch : IEntity
    {
        public Guid Id { get; set; }
        public string CityName { get; set; }

        public Guid GetId() => Id;
        public string GetName() => CityName;
    }
}
