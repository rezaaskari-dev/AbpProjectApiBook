using LibraryMemberService.Localization;
using Volo.Abp.Application.Services;

namespace LibraryMemberService;

/* Inherit your application services from this class.
 */
public abstract class LibraryMemberServiceAppService : ApplicationService
{
    protected LibraryMemberServiceAppService()
    {
        LocalizationResource = typeof(LibraryMemberServiceResource);
    }
}
