using CCMS.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace CCMS.Web.Pages;

public abstract class CCMSPageModel : AbpPageModel
{
    protected CCMSPageModel()
    {
        LocalizationResourceType = typeof(CCMSResource);
    }
}
