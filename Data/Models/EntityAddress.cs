using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class EntityAddress
{
    public Guid Id { get; set; }

    public string? EntityType { get; set; }

    public string? AddressType { get; set; }

    public Guid? Addressid { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? EntityId { get; set; }

    public bool? Isprimary { get; set; }
}
