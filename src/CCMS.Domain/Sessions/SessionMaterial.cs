using System;
using Volo.Abp.Domain.Entities;

namespace CCMS.Sessions;

public class SessionMaterial : Entity<Guid>
{
    public Guid SessionId { get; private set; }
    public Guid RawMaterialId { get; private set; }
    public decimal QtyUsed { get; private set; }

    protected SessionMaterial() { }
    public SessionMaterial(Guid id, Guid sessionId, Guid rawMaterialId, decimal qty) : base(id)
    {
        SessionId = sessionId;
        RawMaterialId = rawMaterialId;
        QtyUsed = qty;
    }
}
