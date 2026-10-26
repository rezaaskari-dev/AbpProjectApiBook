using System.Threading.Tasks;

namespace AbpApiBook.Data;

public interface IAbpApiBookDbSchemaMigrator
{
    Task MigrateAsync();
}
