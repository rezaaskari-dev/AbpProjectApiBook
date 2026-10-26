using LibraryMessaging.Contracts.Members;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace AbpApiBook.Books
{
    public class TestRabbitPublisher : ITransientDependency
    {
        private readonly IDistributedEventBus _eventBus;

        public TestRabbitPublisher(IDistributedEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task PublishAsync(Guid memberId)
        {
            var correlationId = Guid.NewGuid().ToString();

            await _eventBus.PublishAsync(
                new CheckMemberExistsEto
                {
                    MemberId = memberId,
                    CorrelationId = correlationId
                });
        }
    }
}
