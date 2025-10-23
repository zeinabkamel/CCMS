using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace CCMS.Sessions;

public class SessionMaterialDto : EntityDto<Guid>
{
    public Guid SessionId { get; set; }
    public Guid RawMaterialId { get; set; }
    public decimal QtyUsed { get; set; }
}

public class SessionDto : EntityDto<Guid>
{
    public Guid PatientId { get; set; }
    public Guid DoctorStaffId { get; set; }
    public int Type { get; set; }
    public int Status { get; set; }
    public DateTime ScheduledAt { get; set; }
    public int DurationMins { get; set; }
    public string? Notes { get; set; }
    public List<SessionMaterialDto> Materials { get; set; } = new();
}

public class CreateUpdateSessionMaterialDto
{
    [Required] public Guid RawMaterialId { get; set; }
    [Range(0.01, double.MaxValue)] public decimal QtyUsed { get; set; }
}

public class CreateUpdateSessionDto
{
    [Required] public Guid PatientId { get; set; }
    [Required] public Guid DoctorStaffId { get; set; }
    [Required] public int Type { get; set; }
    [Required] public DateTime ScheduledAt { get; set; }
    [Range(5, 1440)] public int DurationMins { get; set; } = 30;
    public string? Notes { get; set; }
    public List<CreateUpdateSessionMaterialDto> Materials { get; set; } = new();
}
