using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace CCMS.Pages;

[Collection(CCMSTestConsts.CollectionDefinitionName)]
public class Index_Tests : CCMSWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
