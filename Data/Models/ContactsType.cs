using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class ContactsType
{
    public Guid Id { get; set; }

    public int ContactTypeId { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Cell { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}
