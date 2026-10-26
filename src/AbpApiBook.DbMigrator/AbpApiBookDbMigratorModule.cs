using AbpApiBook.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AbpApiBook.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpApiBookEntityFrameworkCoreModule),
    typeof(AbpApiBookApplicationContractsModule)
)]
public class AbpApiBookDbMigratorModule : AbpModule
{
}
