using Volo.Abp.Modularity;

namespace LibraryMemberService;

[DependsOn(
    typeof(LibraryMemberServiceDomainModule),
    typeof(LibraryMemberServiceTestBaseModule)
)]
public class LibraryMemberServiceDomainTestModule : AbpModule
{

}
