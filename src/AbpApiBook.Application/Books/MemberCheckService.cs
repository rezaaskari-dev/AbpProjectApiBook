using LibraryMessaging.Contracts.Members;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace AbpApiBook.Books;

public class MemberCheckService : ITransientDependency
{
    private readonly IDistributedEventBus _eventBus;
    private readonly MemberCheckResponseManager _responseManager;

    public MemberCheckService(
        IDistributedEventBus eventBus,
        MemberCheckResponseManager responseManager)
    {
        _eventBus = eventBus;
        _responseManager = responseManager;
    }

    public async Task<bool> CheckMemberAsync(Guid memberId)
    {
        var correlationId = Guid.NewGuid().ToString();

        var responseTask = _responseManager.WaitForResponseAsync(
            correlationId,
            TimeSpan.FromSeconds(10));

        await _eventBus.PublishAsync(
            new CheckMemberExistsEto
            {
                MemberId = memberId,
                CorrelationId = correlationId
            });

        return await responseTask;
    }
}