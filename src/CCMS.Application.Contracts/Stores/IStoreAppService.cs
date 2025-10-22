using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CCMS.Stores;

public interface IStoreAppService :
    ICrudAppService<StoreDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateStoreDto>
{ }
