using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Facility
{
    public Guid Id { get; set; }

    public string? FacilityId { get; set; }

    public string? FacilityName { get; set; }

    public string? AdministeredAtLocation { get; set; }

    public string? SendingOrganization { get; set; }

    public string? ResponsibleOrganization { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public Guid? OrganizationsId { get; set; }

    public virtual Organization? Organizations { get; set; }
}
