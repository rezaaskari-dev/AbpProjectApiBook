using LibraryMessaging.Contracts.Members;
using Rebus.Bus;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AbpApiBook.Books;

public class TestRebusPublisher : ITransientDependency
{
    private readonly IBus _bus;

    public TestRebusPublisher(IBus bus)
    {
        _bus = bus;
    }

    public async Task PublishAsync(Guid memberId)
    {
        var correlationId = Guid.NewGuid().ToString();

        await _bus.Send(new CheckMemberExistsRequest
        {
            MemberId = memberId,
            CorrelationId = correlationId
        });
    }
}