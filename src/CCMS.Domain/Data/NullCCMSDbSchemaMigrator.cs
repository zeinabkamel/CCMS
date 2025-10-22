using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CCMS.Data;

/* This is used if database provider does't define
 * ICCMSDbSchemaMigrator implementation.
 */
public class NullCCMSDbSchemaMigrator : ICCMSDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
