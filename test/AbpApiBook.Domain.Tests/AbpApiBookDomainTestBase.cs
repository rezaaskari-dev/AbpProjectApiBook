using Volo.Abp.Modularity;

namespace AbpApiBook;

/* Inherit from this class for your domain layer tests. */
public abstract class AbpApiBookDomainTestBase<TStartupModule> : AbpApiBookTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
