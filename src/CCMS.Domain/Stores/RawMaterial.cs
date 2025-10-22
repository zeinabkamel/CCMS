using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CCMS.Stores;

public class RawMaterial : FullAuditedAggregateRoot<Guid>
{
    protected RawMaterial() { }
    public RawMaterial(Guid id, string name, string sku, string unit, Guid storeId) : base(id)
    {
        Name = name;
        SKU = sku;
        Unit = unit;
        StoreId = storeId;
    }

    public string Name { get; set; } = default!;
    public string SKU { get; set; } = default!;
    public string Unit { get; set; } = default!;
    public int Quantity { get; set; }
    public int? ReorderLevel { get; set; }
    public decimal? Price { get; set; }
    public string? SupplierName { get; set; }

    public Guid StoreId { get; set; }
    public Store? Store { get; set; }
}
