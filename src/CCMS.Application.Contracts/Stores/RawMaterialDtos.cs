using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace CCMS.Stores;

public class RawMaterialDto : AuditedEntityDto<Guid>
{
    public string Name { get; set; } = default!;
    public string SKU { get; set; } = default!;
    public string Unit { get; set; } = default!;
    public int Quantity { get; set; }
    public int? ReorderLevel { get; set; }
    public decimal? Price { get; set; }
    public string? SupplierName { get; set; }
    public Guid StoreId { get; set; }
    public string? StoreName { get; set; }
}

public class CreateUpdateRawMaterialDto
{
    [Required, StringLength(128)] public string Name { get; set; } = default!;
    [Required, StringLength(64)] public string SKU { get; set; } = default!;
    [Required, StringLength(32)] public string Unit { get; set; } = default!;
    [Range(0, int.MaxValue)] public int Quantity { get; set; }
    public int? ReorderLevel { get; set; }
    public decimal? Price { get; set; }
    [StringLength(128)] public string? SupplierName { get; set; }
    [Required] public Guid StoreId { get; set; }
}
