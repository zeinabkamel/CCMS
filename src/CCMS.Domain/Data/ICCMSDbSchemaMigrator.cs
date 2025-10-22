using System.Threading.Tasks;

namespace CCMS.Data;

public interface ICCMSDbSchemaMigrator
{
    Task MigrateAsync();
}
