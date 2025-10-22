using Volo.Abp.Modularity;

namespace CCMS;

/* Inherit from this class for your domain layer tests. */
public abstract class CCMSDomainTestBase<TStartupModule> : CCMSTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
