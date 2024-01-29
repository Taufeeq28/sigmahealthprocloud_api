using System.ComponentModel.DataAnnotations;

namespace BAL.Request
{
    public class CreateEntityAddressRequest
    {

        [Required]
        public string EntityType { get; set; }

        [Required]
        public string AddressType { get; set; }

        [Required]
        public Guid Addressid { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public Guid EntityId { get; set; }

        [Required]
        public bool? Isprimary { get; set; }
    }
}
