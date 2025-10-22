using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using CCMS.Localization;

namespace CCMS.Web;

[Dependency(ReplaceServices = true)]
public class CCMSBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<CCMSResource> _localizer;

    public CCMSBrandingProvider(IStringLocalizer<CCMSResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
