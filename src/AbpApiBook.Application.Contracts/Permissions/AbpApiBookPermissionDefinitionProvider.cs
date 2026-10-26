using AbpApiBook.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace AbpApiBook.Permissions;

public class AbpApiBookPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AbpApiBookPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(AbpApiBookPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AbpApiBookResource>(name);
    }
}
