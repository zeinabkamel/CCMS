using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CCMS.Stores;

public class Store : FullAuditedAggregateRoot<Guid>
{
    protected Store() { }
    public Store(Guid id, string name, string? location = null, string? description = null) : base(id)
    {
        Name = name;
        Location = location;
        Description = description;
    }

    public string Name { get; set; } = default!;
    public string? Location { get; set; }
    public string? Description { get; set; }
}
