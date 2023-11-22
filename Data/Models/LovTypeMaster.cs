using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class LovTypeMaster
{
    public int ReferenceId { get; set; }

    public string? Key { get; set; }

    public string? Value { get; set; }

    public string? LovType { get; set; }
}
