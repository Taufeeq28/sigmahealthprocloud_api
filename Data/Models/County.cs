using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class County
{
    public Guid Id { get; set; }

    public int? CountyId { get; set; }

    public string? CountyName { get; set; }

    public string? CountyCode { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? CreatedBy { get; set; }

    public DateTime? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public Guid? StateId { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual State? State { get; set; }
}
