using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CvxVisMaster
{
    public string? CvxCode { get; set; }

    public string? CvxVaccineDescription { get; set; }

    public string? VisDocumentName { get; set; }

    public string? VisFullyEncodedTextString { get; set; }

    public DateOnly? VisEditionDate { get; set; }

    public string? VisEditionStatus { get; set; }
}
