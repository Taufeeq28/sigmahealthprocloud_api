using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class User
{
    public Guid Id { get; set; }

    public int? UsersId { get; set; }

    public string? UserFirstname { get; set; }

    public string? UserLasttname { get; set; }

    public string? Designation { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public Guid? UserTypeId { get; set; }

    public virtual UserType? UserType { get; set; }
}
