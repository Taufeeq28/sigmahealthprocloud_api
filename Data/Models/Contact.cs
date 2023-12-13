using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Contact
{
    public Guid Id { get; set; }

    public int ContactsId { get; set; }

    public string? ContactValue { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }
}
