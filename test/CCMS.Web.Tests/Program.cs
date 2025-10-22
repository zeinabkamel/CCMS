using Microsoft.AspNetCore.Builder;
using CCMS;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("CCMS.Web.csproj"); 
await builder.RunAbpModuleAsync<CCMSWebTestModule>(applicationName: "CCMS.Web");

public partial class Program
{
}
