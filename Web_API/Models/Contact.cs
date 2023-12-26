using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Contact
{
    public Guid Id { get; set; }

    public string ContactsId { get; set; } = null!;

    public string? ContactValue { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public string? ContactType { get; set; }

    public virtual ICollection<Facility> Facilities { get; set; } = new List<Facility>();

    public virtual ICollection<Site> Sites { get; set; } = new List<Site>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
