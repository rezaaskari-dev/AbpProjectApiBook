using Volo.Abp.Modularity;

namespace AbpApiBook;

[DependsOn(
    typeof(AbpApiBookApplicationModule),
    typeof(AbpApiBookDomainTestModule)
)]
public class AbpApiBookApplicationTestModule : AbpModule
{

}
