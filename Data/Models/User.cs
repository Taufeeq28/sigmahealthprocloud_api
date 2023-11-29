using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string? UserFirstname { get; set; }

    public string? UserLasttname { get; set; }

    public string? Designation { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public Guid? LovMasterId { get; set; }

    public string? Password { get; set; }

    public string? UserId { get; set; }

    public virtual LovMaster? LovMaster { get; set; }
}
