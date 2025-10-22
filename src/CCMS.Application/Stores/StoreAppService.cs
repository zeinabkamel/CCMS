using System;
using CCMS.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CCMS.Stores;

public class StoreAppService :
    CrudAppService<Store, StoreDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateStoreDto>,
    IStoreAppService
{
    public StoreAppService(IRepository<Store, Guid> repo) : base(repo) { }

    protected override string GetPolicyName => CCMSPermissions.Store.Default;
    protected override string GetListPolicyName => CCMSPermissions.Store.Default;
    protected override string CreatePolicyName => CCMSPermissions.Store.Create;
    protected override string UpdatePolicyName => CCMSPermissions.Store.Update;
    protected override string DeletePolicyName => CCMSPermissions.Store.Delete;
}
