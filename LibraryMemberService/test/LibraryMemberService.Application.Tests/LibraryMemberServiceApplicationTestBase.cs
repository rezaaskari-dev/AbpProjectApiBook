using Volo.Abp.Modularity;

namespace LibraryMemberService;

public abstract class LibraryMemberServiceApplicationTestBase<TStartupModule> : LibraryMemberServiceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
