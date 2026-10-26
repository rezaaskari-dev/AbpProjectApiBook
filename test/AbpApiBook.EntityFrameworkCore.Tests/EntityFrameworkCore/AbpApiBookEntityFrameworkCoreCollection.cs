using Xunit;

namespace AbpApiBook.EntityFrameworkCore;

[CollectionDefinition(AbpApiBookTestConsts.CollectionDefinitionName)]
public class AbpApiBookEntityFrameworkCoreCollection : ICollectionFixture<AbpApiBookEntityFrameworkCoreFixture>
{

}
