using LibraryMemberService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace LibraryMemberService.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class LibraryMemberServiceController : AbpControllerBase
{
    protected LibraryMemberServiceController()
    {
        LocalizationResource = typeof(LibraryMemberServiceResource);
    }
}
