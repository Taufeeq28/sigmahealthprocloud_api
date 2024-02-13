using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Person
{
    public Guid? Id { get; set; }

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

    public string? MiddleName { get; set; }

    public string? MotherFirstName { get; set; }

    public string? MotherLastName { get; set; }

    public string? MotherMaidenLastName { get; set; }

    public string? BirthOrder { get; set; }

    public Guid? BirthStateId { get; set; }

    public virtual State? BirthState { get; set; }

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
