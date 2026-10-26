using Microsoft.Extensions.Localization;
using AbpApiBook.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace AbpApiBook;

[Dependency(ReplaceServices = true)]
public class AbpApiBookBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<AbpApiBookResource> _localizer;

    public AbpApiBookBrandingProvider(IStringLocalizer<AbpApiBookResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];

    public override string? LogoUrl => "~/images/logo/leptonxlite/logo-light-thumbnail.png";

    public override string? LogoIconUrl => "~/images/logo/leptonxlite/logo-dark-thumbnail.png";
}
