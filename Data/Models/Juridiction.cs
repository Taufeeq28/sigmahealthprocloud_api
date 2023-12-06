using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Juridiction
{
    public Guid Id { get; set; }

    public string? JuridictionId { get; set; }

    public string? JuridictionName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public Guid? StateId { get; set; }

    public Guid? AlternateId { get; set; }

    public virtual BusinessConfiguration? Alternate { get; set; }

    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();

    public virtual State? State { get; set; }
}
