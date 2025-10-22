using System;
using CCMS.Enums;
using Volo.Abp.Domain.Entities;

namespace CCMS.Stores;

public class RawMaterial : Entity<Guid>
{
    public Guid StoreId { get; private set; }
    public string Name { get; private set; } = default!;
    public string SKU { get; private set; } = default!;
    public UnitType Unit { get; private set; }
    public decimal ReorderLevel { get; private set; }
    public decimal CurrentQty { get; private set; }

    protected RawMaterial() { }

    public RawMaterial(Guid id, Guid storeId, string name, string sku, UnitType unit, decimal initialQty, decimal reorderLevel = 0)
        : base(id)
    {
        StoreId = storeId;
        Name = name;
        SKU = sku;
        Unit = unit;
        CurrentQty = initialQty;
        ReorderLevel = reorderLevel;
    }

    public void Increase(decimal qty) => CurrentQty += qty;
    public void Decrease(decimal qty)
    {
        if (qty <= 0) return;
        if (CurrentQty - qty < 0) throw new Volo.Abp.BusinessException("Inventory:Insufficient");
        CurrentQty -= qty;
    }
}
