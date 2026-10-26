using AbpApiBook.Samples;
using Xunit;

namespace AbpApiBook.EntityFrameworkCore.Domains;

[Collection(AbpApiBookTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<AbpApiBookEntityFrameworkCoreTestModule>
{

}
