using CCMS.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CCMS.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class CCMSController : AbpControllerBase
{
    protected CCMSController()
    {
        LocalizationResource = typeof(CCMSResource);
    }
}
