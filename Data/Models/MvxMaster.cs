using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class MvxMaster
{
    public string? MvxCode { get; set; }

    public string? ManufacturerName { get; set; }

    public string? Notes { get; set; }

    public string? Status { get; set; }

    public DateOnly? Lastupdateddate { get; set; }

    public string? ManufacturerId { get; set; }
}
