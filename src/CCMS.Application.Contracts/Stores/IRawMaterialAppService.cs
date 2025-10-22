using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CCMS.Stores;

public interface IRawMaterialAppService :
    ICrudAppService<RawMaterialDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateRawMaterialDto>
{ }
