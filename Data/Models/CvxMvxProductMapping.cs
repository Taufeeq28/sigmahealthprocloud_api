using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CvxMvxProductMapping
{
    public string? ProductTblProductName { get; set; }

    public string? CvxShortDescription { get; set; }

    public string? CvxCode { get; set; }

    public string? ManufacturerTblManufacturerName { get; set; }

    public string? ManufacturerTblMvxCode { get; set; }

    public string? MvxStatus { get; set; }

    public string? ProductNameStatus { get; set; }

    public DateOnly? ProductTblUpdateDate { get; set; }
}
