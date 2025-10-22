using CCMS.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace CCMS.Sessions;

/// <summary>
///  EfCoreRepository  get the defualt methods  of repository and other  custimized from  ISessionRepoistory
/// </summary>
public class EfCoreSessionRepository : EfCoreRepository<CCMSDbContext, Session, Guid>, ISessionRepository
{
    public EfCoreSessionRepository(IDbContextProvider<CCMSDbContext> dbContextProvider) : base(dbContextProvider) { }

    public async Task<Session> GetWithDetailsAsync(Guid id)
    {
        var db = await GetDbContextAsync();
        return await db.Set<Session>()
            .Include(x => x.Materials)
            .FirstAsync(x => x.Id == id);
    }
}
