using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AbpApiBook.Data;

/* This is used if database provider does't define
 * IAbpApiBookDbSchemaMigrator implementation.
 */
public class NullAbpApiBookDbSchemaMigrator : IAbpApiBookDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
