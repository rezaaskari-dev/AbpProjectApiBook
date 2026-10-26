using Volo.Abp.Modularity;

namespace AbpApiBook;

public abstract class AbpApiBookApplicationTestBase<TStartupModule> : AbpApiBookTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
