using CCMS.Localization;
using Volo.Abp.Application.Services;

namespace CCMS;

/* Inherit your application services from this class.
 */
public abstract class CCMSAppService : ApplicationService
{
    protected CCMSAppService()
    {
        LocalizationResource = typeof(CCMSResource);
    }
}
