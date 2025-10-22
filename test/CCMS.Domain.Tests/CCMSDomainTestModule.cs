using Volo.Abp.Modularity;

namespace CCMS;

[DependsOn(
    typeof(CCMSDomainModule),
    typeof(CCMSTestBaseModule)
)]
public class CCMSDomainTestModule : AbpModule
{

}
