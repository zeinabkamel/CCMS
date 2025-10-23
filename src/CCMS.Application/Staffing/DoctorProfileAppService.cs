using System;
using CCMS.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CCMS.Staffing;

public class DoctorProfileAppService :
    CrudAppService<DoctorProfile, DoctorProfileDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateDoctorProfileDto>,
    IDoctorProfileAppService
{
    public DoctorProfileAppService(IRepository<DoctorProfile, Guid> repo) : base(repo) { }

    protected override string GetPolicyName => CCMSPermissions.Doctors.Default;
    protected override string GetListPolicyName => CCMSPermissions.Doctors.Default;
    protected override string CreatePolicyName => CCMSPermissions.Doctors.Create;
    protected override string UpdatePolicyName => CCMSPermissions.Doctors.Update;
    protected override string DeletePolicyName => CCMSPermissions.Doctors.Delete;
}
