using Volo.Abp.Settings;

namespace CCMS.Settings;

public class CCMSSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(CCMSSettings.MySetting1));
    }
}
