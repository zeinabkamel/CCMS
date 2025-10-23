using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CCMS.Sessions;

public interface ISessionAppService :
    ICrudAppService<SessionDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateSessionDto>
{
    Task RescheduleAsync(Guid id, DateTime newDate);
    Task CompleteAsync(Guid id, string? notes);
    Task CancelAsync(Guid id, string? notes);
}
