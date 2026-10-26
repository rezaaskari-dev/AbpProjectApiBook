using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AbpApiBook.Data;
using Volo.Abp.DependencyInjection;

namespace AbpApiBook.EntityFrameworkCore;

public class EntityFrameworkCoreAbpApiBookDbSchemaMigrator
    : IAbpApiBookDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreAbpApiBookDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the AbpApiBookDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<AbpApiBookDbContext>()
            .Database
            .MigrateAsync();
    }
}
