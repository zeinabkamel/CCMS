using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
 using CCMS.Enums;

namespace CCMS.Sessions;

public class Session : AggregateRoot<Guid>
{
    public Guid PatientId { get; private set; }
    public Guid DoctorStaffId { get; private set; }
    public SessionType Type { get; private set; }
    public SessionStatus Status { get; private set; }
    public DateTime ScheduledAt { get; private set; }
    public int DurationMins { get; private set; }
    public string? Notes { get; private set; }

    private readonly List<SessionMaterial> _materials = new();
    public virtual IReadOnlyCollection<SessionMaterial> Materials => _materials.AsReadOnly();

    protected Session() { }

    public Session(Guid id, Guid patientId, Guid doctorStaffId, SessionType type, DateTime scheduledAt, int durationMins)
        : base(id)
    {
        PatientId = patientId;
        DoctorStaffId = doctorStaffId;
        Type = type;
        Status = SessionStatus.Scheduled;
        ScheduledAt = scheduledAt;
        DurationMins = durationMins;
    }

    public void Reschedule(DateTime newDate)
    {
        if (newDate < DateTime.Now) throw new BusinessException("Session:PastDate");
        ScheduledAt = newDate;
    }

    public void AddMaterial(Guid rawMaterialId, decimal qty)
    {
        if (qty <= 0) throw new BusinessException("Material:QtyMustBePositive");
        _materials.Add(new SessionMaterial(Guid.NewGuid(), Id, rawMaterialId, qty));
    }

    public void Complete(string? notes = null)
    {
        Status = SessionStatus.Completed;
        Notes = notes;
    }

    public void Cancel(string? notes = null)
    {
        Status = SessionStatus.Cancelled;
        Notes = notes;
    }
}
