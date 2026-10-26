using LibraryMemberService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace LibraryMemberService.Permissions;

public class LibraryMemberServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(LibraryMemberServicePermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(LibraryMemberServicePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LibraryMemberServiceResource>(name);
    }
}
