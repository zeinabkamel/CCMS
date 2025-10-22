using CCMS.Books;
using Xunit;

namespace CCMS.EntityFrameworkCore.Applications.Books;

[Collection(CCMSTestConsts.CollectionDefinitionName)]
public class EfCoreBookAppService_Tests : BookAppService_Tests<CCMSEntityFrameworkCoreTestModule>
{

}