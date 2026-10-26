using LibraryMemberService.Samples;
using Xunit;

namespace LibraryMemberService.EntityFrameworkCore.Applications;

[Collection(LibraryMemberServiceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<LibraryMemberServiceEntityFrameworkCoreTestModule>
{

}
