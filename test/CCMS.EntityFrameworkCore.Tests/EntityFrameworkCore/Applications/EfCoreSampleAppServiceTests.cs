using CCMS.Samples;
using Xunit;

namespace CCMS.EntityFrameworkCore.Applications;

[Collection(CCMSTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<CCMSEntityFrameworkCoreTestModule>
{

}
