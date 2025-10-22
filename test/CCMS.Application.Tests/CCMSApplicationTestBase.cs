using Volo.Abp.Modularity;

namespace CCMS;

public abstract class CCMSApplicationTestBase<TStartupModule> : CCMSTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
