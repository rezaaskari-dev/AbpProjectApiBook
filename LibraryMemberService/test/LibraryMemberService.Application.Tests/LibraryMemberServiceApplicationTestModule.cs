using Volo.Abp.Modularity;

namespace LibraryMemberService;

[DependsOn(
    typeof(LibraryMemberServiceApplicationModule),
    typeof(LibraryMemberServiceDomainTestModule)
)]
public class LibraryMemberServiceApplicationTestModule : AbpModule
{

}
