
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace BAL.Request
{
    public class UpdateEntiyContactRequest
    {
        [Required]
        public Guid ContactId { get; set; }
        public string? NewContactValue { get; set; }

        [Required]
        public string UpdatedBy { get; set; }

        public string? NewContactType { get; set; }

        [DefaultValue(false)]
        public bool? isDelete { get; set; }
    }
}
