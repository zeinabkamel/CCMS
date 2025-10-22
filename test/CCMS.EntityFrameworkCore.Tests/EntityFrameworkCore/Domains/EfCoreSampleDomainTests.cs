using CCMS.Samples;
using Xunit;

namespace CCMS.EntityFrameworkCore.Domains;

[Collection(CCMSTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<CCMSEntityFrameworkCoreTestModule>
{

}
