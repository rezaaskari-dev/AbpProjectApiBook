using Volo.Abp.Modularity;

namespace AbpApiBook;

[DependsOn(
    typeof(AbpApiBookDomainModule),
    typeof(AbpApiBookTestBaseModule)
)]
public class AbpApiBookDomainTestModule : AbpModule
{

}
