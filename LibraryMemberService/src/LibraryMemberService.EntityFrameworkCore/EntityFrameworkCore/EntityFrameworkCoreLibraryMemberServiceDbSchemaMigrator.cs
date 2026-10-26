using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LibraryMemberService.Data;
using Volo.Abp.DependencyInjection;

namespace LibraryMemberService.EntityFrameworkCore;

public class EntityFrameworkCoreLibraryMemberServiceDbSchemaMigrator
    : ILibraryMemberServiceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreLibraryMemberServiceDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the LibraryMemberServiceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<LibraryMemberServiceDbContext>()
            .Database
            .MigrateAsync();
    }
}
