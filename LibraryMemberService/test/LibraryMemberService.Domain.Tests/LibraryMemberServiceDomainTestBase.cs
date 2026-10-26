using Volo.Abp.Modularity;

namespace LibraryMemberService;

/* Inherit from this class for your domain layer tests. */
public abstract class LibraryMemberServiceDomainTestBase<TStartupModule> : LibraryMemberServiceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
