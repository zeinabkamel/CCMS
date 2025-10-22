using Volo.Abp.Modularity;

namespace CCMS;

[DependsOn(
    typeof(CCMSApplicationModule),
    typeof(CCMSDomainTestModule)
)]
public class CCMSApplicationTestModule : AbpModule
{

}
