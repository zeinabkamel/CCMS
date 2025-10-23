using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace CCMS.Staffing;

public class DoctorProfileDto : EntityDto<Guid>
{
    public Guid StaffId { get; set; }
    public string? Specialty { get; set; }
    public string? LicenseNo { get; set; }
    public string? Bio { get; set; }
}

public class StaffDto : EntityDto<Guid>
{
    public Guid? UserId { get; set; }
    public string FullName { get; set; } = default!;
    public int Role { get; set; }
    public string? Phone { get; set; }
    public bool IsActive { get; set; }
    public Guid? DoctorProfileId { get; set; }
}

public class CreateUpdateStaffDto
{
    [Required, StringLength(128)] public string FullName { get; set; } = default!;
    [Required] public int Role { get; set; }
    [StringLength(32)] public string? Phone { get; set; }
    public Guid? UserId { get; set; }
}

public class CreateUpdateDoctorProfileDto
{
    [Required] public Guid StaffId { get; set; }
    public string? Specialty { get; set; }
    public string? LicenseNo { get; set; }
    public string? Bio { get; set; }
}
