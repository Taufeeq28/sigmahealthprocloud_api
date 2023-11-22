using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class JuridictionsOrganizationAssociation
{
    public Guid Id { get; set; }

    public int JuridictionsOrganizationId { get; set; }

    public int OrganizationId { get; set; }

    public int JurisdictionId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public virtual ICollection<Facility> Facilities { get; set; } = new List<Facility>();

    public virtual Jurisdiction Jurisdiction { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;
}
