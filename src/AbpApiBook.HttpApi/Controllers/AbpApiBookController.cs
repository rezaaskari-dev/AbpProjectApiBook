using AbpApiBook.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AbpApiBook.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class AbpApiBookController : AbpControllerBase
{
    protected AbpApiBookController()
    {
        LocalizationResource = typeof(AbpApiBookResource);
    }
}
