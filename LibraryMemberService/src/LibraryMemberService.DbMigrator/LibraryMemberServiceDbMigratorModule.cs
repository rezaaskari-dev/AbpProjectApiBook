using LibraryMemberService.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace LibraryMemberService.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(LibraryMemberServiceEntityFrameworkCoreModule),
    typeof(LibraryMemberServiceApplicationContractsModule)
)]
public class LibraryMemberServiceDbMigratorModule : AbpModule
{
}
