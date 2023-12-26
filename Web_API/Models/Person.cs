using System;
using System.Collections.Generic;

namespace Web_API.Models;

public partial class Person
{
    public Guid Id { get; set; }

    public int PersonId { get; set; }

    public string? PersonType { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Gender { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public string? DateOfBirth { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
