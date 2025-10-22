using System;
using Volo.Abp.Application.Dtos;

namespace CCMS.Patients;

public class PatientDto : EntityDto<Guid>
{
    public string Code { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string? Gender { get; set; }
    public string? Notes { get; set; }
}

public class CreateUpdatePatientDto
{
    public string Code { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string? Gender { get; set; }
    public string? Notes { get; set; }
}
