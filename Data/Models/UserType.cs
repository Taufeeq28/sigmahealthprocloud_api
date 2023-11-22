using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class UserType
{
    public Guid Id { get; set; }

    public string? UserTypesName { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public string? UserTypeId { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
