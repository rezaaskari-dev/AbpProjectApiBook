using Microsoft.Extensions.Localization;
using LibraryMemberService.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace LibraryMemberService;

[Dependency(ReplaceServices = true)]
public class LibraryMemberServiceBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<LibraryMemberServiceResource> _localizer;

    public LibraryMemberServiceBrandingProvider(IStringLocalizer<LibraryMemberServiceResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];

    public override string? LogoUrl => "~/images/logo/leptonxlite/logo-light-thumbnail.png";

    public override string? LogoIconUrl => "~/images/logo/leptonxlite/logo-dark-thumbnail.png";
}
