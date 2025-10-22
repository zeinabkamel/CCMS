using CCMS.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace CCMS.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CCMSEntityFrameworkCoreModule),
    typeof(CCMSApplicationContractsModule)
)]
public class CCMSDbMigratorModule : AbpModule
{
}
