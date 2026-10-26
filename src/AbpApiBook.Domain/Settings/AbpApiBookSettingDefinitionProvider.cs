using Volo.Abp.Settings;

namespace AbpApiBook.Settings;

public class AbpApiBookSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(AbpApiBookSettings.MySetting1));
    }
}
