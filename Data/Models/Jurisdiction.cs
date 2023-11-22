using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Jurisdiction
{
    public Guid Id { get; set; }

    public int JurisdictionId { get; set; }

    public string? JurisdictionName { get; set; }

    public int Zipcode { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public virtual ICollection<JuridictionsOrganizationAssociation> JuridictionsOrganizationAssociations { get; set; } = new List<JuridictionsOrganizationAssociation>();
}
