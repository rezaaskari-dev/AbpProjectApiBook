using System;
using System.Threading.Tasks;
using LibraryMessaging.Contracts.Members;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace LibraryMemberService.Members;

public class CheckMemberExistsHandler :
    IDistributedEventHandler<CheckMemberExistsEto>,
    ITransientDependency
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IDistributedEventBus _eventBus;

    public CheckMemberExistsHandler(
        IServiceScopeFactory scopeFactory,
        IDistributedEventBus eventBus)
    {
        _scopeFactory = scopeFactory;
        _eventBus = eventBus;
    }

    public async Task HandleEventAsync(CheckMemberExistsEto eventData)
    {
        bool exists;

        using (var scope = _scopeFactory.CreateScope())
        {
            var uowManager = scope.ServiceProvider
                .GetRequiredService<IUnitOfWorkManager>();

            using (var uow = uowManager.Begin(
                requiresNew: true,
                isTransactional: false))
            {
                var memberRepository = scope.ServiceProvider
                    .GetRequiredService<IRepository<Member, Guid>>();

                exists = await memberRepository.AnyAsync(
                    member => member.Id == eventData.MemberId);

                await uow.CompleteAsync();
            }
        }

        await _eventBus.PublishAsync(new CheckMemberExistsResultEto
        {
            CorrelationId = eventData.CorrelationId,
            MemberId = eventData.MemberId,
            Exists = exists
        });
    }
}