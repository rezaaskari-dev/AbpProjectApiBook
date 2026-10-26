using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LibraryMemberService.Data;

/* This is used if database provider does't define
 * ILibraryMemberServiceDbSchemaMigrator implementation.
 */
public class NullLibraryMemberServiceDbSchemaMigrator : ILibraryMemberServiceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
