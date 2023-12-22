using Data.Models;


namespace BAL.Responses
{
    public class FacilityDetailsResponse
    {
        public string? FacilityName { get; set; }
        public string? AdministeredAtLocation { get; set; }
        public string? SendingOrganization { get; set; }
        public string? ResponsibleOrganization { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public Guid? OrganizationsId { get; set; }
        public Guid? AddressId { get; set; }

        public static FacilityDetailsResponse FromFacilityEntity(Facility facility)
        {
            return new FacilityDetailsResponse
            {
                FacilityName = facility.FacilityName,
                AdministeredAtLocation = facility.AdministeredAtLocation,
                SendingOrganization = facility.SendingOrganization,
                ResponsibleOrganization = facility.ResponsibleOrganization,
                UpdatedDate = facility.UpdatedDate,
                UpdatedBy = facility.UpdatedBy,
                OrganizationsId = facility.OrganizationsId,
                AddressId = facility.AddressId
            };
        }
    }
}
