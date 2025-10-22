using Xunit;

namespace CCMS.EntityFrameworkCore;

[CollectionDefinition(CCMSTestConsts.CollectionDefinitionName)]
public class CCMSEntityFrameworkCoreCollection : ICollectionFixture<CCMSEntityFrameworkCoreFixture>
{

}
