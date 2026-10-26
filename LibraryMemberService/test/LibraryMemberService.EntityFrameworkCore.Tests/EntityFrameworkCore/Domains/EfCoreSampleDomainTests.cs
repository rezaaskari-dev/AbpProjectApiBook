using LibraryMemberService.Samples;
using Xunit;

namespace LibraryMemberService.EntityFrameworkCore.Domains;

[Collection(LibraryMemberServiceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<LibraryMemberServiceEntityFrameworkCoreTestModule>
{

}
