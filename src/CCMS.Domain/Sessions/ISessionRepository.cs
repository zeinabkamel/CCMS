using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace CCMS.Sessions;

/// <summary>
/// naming convention
/// </summary>
public interface ISessionRepository : IRepository<Session, Guid>
{
    Task<Session> GetWithDetailsAsync(Guid id);
}
