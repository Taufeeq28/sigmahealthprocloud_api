using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class BusinessConfiguration
{
    public Guid Id { get; set; }

    public string BusinessId { get; set; } = null!;

    public string? BusinessName { get; set; }

    public string? EmailId { get; set; }

    public string? Password { get; set; }

    public string? JusrisidictionIdSuffix { get; set; }

    public string? JurisdictionIdStart { get; set; }

    public string? FaciltyIdSuffix { get; set; }

    public string? FaciltyIdStart { get; set; }

    public string? OrganizationIdSuffix { get; set; }

    public string? OrganizationIdStart { get; set; }

    public string? AddressIdSuffix { get; set; }

    public string? AddressIdStart { get; set; }

    public virtual ICollection<Juridiction> Juridictions { get; set; } = new List<Juridiction>();
}
