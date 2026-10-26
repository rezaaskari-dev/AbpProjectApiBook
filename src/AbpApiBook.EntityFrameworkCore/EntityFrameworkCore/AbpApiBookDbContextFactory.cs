using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AbpApiBook.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class AbpApiBookDbContextFactory : IDesignTimeDbContextFactory<AbpApiBookDbContext>
{
    public AbpApiBookDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        AbpApiBookEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<AbpApiBookDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new AbpApiBookDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../AbpApiBook.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}
