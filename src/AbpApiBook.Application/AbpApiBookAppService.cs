using AbpApiBook.Localization;
using Volo.Abp.Application.Services;

namespace AbpApiBook;

/* Inherit your application services from this class.
 */
public abstract class AbpApiBookAppService : ApplicationService
{
    protected AbpApiBookAppService()
    {
        LocalizationResource = typeof(AbpApiBookResource);
    }
}
