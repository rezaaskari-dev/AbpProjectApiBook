using AbpApiBook.MinIO;
using AbpApiBook.MinIO.AbpApiBook.MinIO;
using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;
using Polly;
using System;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace AbpApiBook;

[DependsOn(
    typeof(AbpApiBookDomainModule),
    typeof(AbpApiBookApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class AbpApiBookApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        context.Services.AddTransient<
            IFileStorageService,
            BookFileStorageService>();


        Configure<MinioOptions>(
    context.Services.GetConfiguration()
        .GetSection(MinioOptions.SectionName)
);
        context.Services.AddTransient<MinioHealthCheckService>();

        context.Services.AddSingleton<IMinioClient>(serviceProvider =>
        {
            var options = serviceProvider
                .GetRequiredService<IOptions<MinioOptions>>()
                .Value;

            return new MinioClient()
                .WithEndpoint(options.Endpoint)
                .WithCredentials(options.AccessKey, options.SecretKey)
                .WithSSL(options.UseSSL)
                .Build();
        });
        //context.Services.AddTransient<BookFileStorageService>();

        context.Services.AddSingleton<ElasticsearchClient>(sp =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            var url = configuration["Elasticsearch:Url"]
                      ?? "http://localhost:9200";

            var settings = new ElasticsearchClientSettings(new Uri(url));

            return new ElasticsearchClient(settings);
        });


    }
}
