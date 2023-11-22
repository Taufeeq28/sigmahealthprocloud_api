using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class Juridiction
{
    public Guid Id { get; set; }

    public string? JuridictionId { get; set; }

    public string? JuridictionName { get; set; }

    public int? Zipcode { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public bool? Isdelete { get; set; }

    public Guid? BusinessId { get; set; }

    public virtual BusinessConfiguration? Business { get; set; }
}
