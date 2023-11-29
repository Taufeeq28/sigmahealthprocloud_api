using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class City
{
    public Guid Id { get; set; }

    public string? CityName { get; set; }

    public int? Zipcode { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public Guid? CountyId { get; set; }

    public Guid? StateId { get; set; }

    public int? CityId { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual County? County { get; set; }

    public virtual State? State { get; set; }
}
