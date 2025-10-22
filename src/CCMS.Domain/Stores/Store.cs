using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace CCMS.Stores;

public class Store : AggregateRoot<Guid>
{
    public string Name { get; private set; }
    public string? Location { get; private set; }
    public bool IsActive { get; private set; } = true;

    private readonly List<RawMaterial> _materials = new();
    public virtual IReadOnlyCollection<RawMaterial> Materials => _materials.AsReadOnly();

    protected Store() { }
    public Store(Guid id, string name, string? location = null) : base(id)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), maxLength: 128);
        Location = location;
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}
