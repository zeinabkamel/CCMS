using System;
using System.Linq;
using System.Threading.Tasks;
using CCMS.Permissions;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CCMS.Stores;

public class RawMaterialAppService :
    CrudAppService<RawMaterial, RawMaterialDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateRawMaterialDto>,
    IRawMaterialAppService
{
    public RawMaterialAppService(IRepository<RawMaterial, Guid> repo) : base(repo) { }

    protected override string GetPolicyName => CCMSPermissions.RawMaterials.Default;
    protected override string GetListPolicyName => CCMSPermissions.RawMaterials.Default;
    protected override string CreatePolicyName => CCMSPermissions.RawMaterials.Create;
    protected override string UpdatePolicyName => CCMSPermissions.RawMaterials.Update;
    protected override string DeletePolicyName => CCMSPermissions.RawMaterials.Delete;

    //protected override IQueryable<RawMaterial> CreateFilteredQuery(PagedAndSortedResultRequestDto input)
    //    => Repository.WithDetails(x => x.Store);

    public override async Task<RawMaterialDto> GetAsync(Guid id)
    {
        var entity = await Repository.WithDetails(x => x.Store).FirstAsync(x => x.Id == id);
        return MapToGetOutputDto(entity);
    }

    protected override RawMaterialDto MapToGetOutputDto(RawMaterial entity)
    {
        var dto = base.MapToGetOutputDto(entity);
        dto.StoreName = entity.Store?.Name;
        return dto;
    }
}
