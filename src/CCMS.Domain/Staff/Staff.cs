using System;
using CCMS.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
 
namespace CCMS.Staffing;

public class Staff : AggregateRoot<Guid>
{
    public Guid? UserId { get; private set; } // link to IdentityUser
    public string FullName { get; private set; }
    public StaffRole Role { get; private set; }
    public string? Phone { get; private set; }
    public bool IsActive { get; private set; } = true;

    public Guid? DoctorProfileId { get; private set; }
    public virtual DoctorProfile? DoctorProfile { get; private set; }

    protected Staff() { }

    public Staff(Guid id, string fullName, StaffRole role) : base(id)
    {
        FullName = Check.NotNullOrWhiteSpace(fullName, nameof(fullName), maxLength: 128);
        Role = role;
    }

    public void LinkUser(Guid userId) => UserId = userId;
    public void SetPhone(string? phone) => Phone = phone;
    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}
