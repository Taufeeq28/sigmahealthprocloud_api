using System.ComponentModel.DataAnnotations;

namespace BAL.Request
{
    public class UpdateEntityAddressRequest
    {

        [Required]
        public Guid id { get; set; }
        public string? EntityType { get; set; }

        public string? AddressType { get; set; }
       
        public Guid? Addressid { get; set; }

        public string? UpdatedBy { get; set; }
       
        public Guid? EntityId { get; set; }
       
        public bool? Isprimary { get; set; }
    }
}
