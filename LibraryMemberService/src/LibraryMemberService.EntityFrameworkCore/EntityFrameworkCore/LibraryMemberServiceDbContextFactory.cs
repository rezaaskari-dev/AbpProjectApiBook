using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LibraryMemberService.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class LibraryMemberServiceDbContextFactory : IDesignTimeDbContextFactory<LibraryMemberServiceDbContext>
{
    public LibraryMemberServiceDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        LibraryMemberServiceEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<LibraryMemberServiceDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new LibraryMemberServiceDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../LibraryMemberService.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}
