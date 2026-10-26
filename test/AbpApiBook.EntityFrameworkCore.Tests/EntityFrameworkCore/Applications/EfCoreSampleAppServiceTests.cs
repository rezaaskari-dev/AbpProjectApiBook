using AbpApiBook.Samples;
using Xunit;

namespace AbpApiBook.EntityFrameworkCore.Applications;

[Collection(AbpApiBookTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<AbpApiBookEntityFrameworkCoreTestModule>
{

}
