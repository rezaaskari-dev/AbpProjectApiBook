using System.Threading.Tasks;

namespace LibraryMemberService.Data;

public interface ILibraryMemberServiceDbSchemaMigrator
{
    Task MigrateAsync();
}
