using System;
using CCMS.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CCMS.Staffing;

public class StaffAppService :
    CrudAppService<Staff, StaffDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateStaffDto>,
    IStaffAppService
{
    public StaffAppService(IRepository<Staff, Guid> repo) : base(repo) { }

    protected override string GetPolicyName => CCMSPermissions.Staff.Default;
    protected override string GetListPolicyName => CCMSPermissions.Staff.Default;
    protected override string CreatePolicyName => CCMSPermissions.Staff.Create;
    protected override string UpdatePolicyName => CCMSPermissions.Staff.Update;
    protected override string DeletePolicyName => CCMSPermissions.Staff.Delete;
}
