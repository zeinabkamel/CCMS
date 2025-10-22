using System;
using Volo.Abp.Domain.Entities;

namespace CCMS.Staffing;

public class DoctorProfile : Entity<Guid>
{
    public Guid StaffId { get; private set; }
    public string? Specialty { get; private set; }
    public string? LicenseNo { get; private set; }
    public string? Bio { get; private set; }

    protected DoctorProfile() { }
    public DoctorProfile(Guid id, Guid staffId) : base(id)
    {
        StaffId = staffId;
    }

    public void Update(string? specialty, string? licenseNo, string? bio)
    {
        Specialty = specialty;
        LicenseNo = licenseNo;
        Bio = bio;
    }
}
