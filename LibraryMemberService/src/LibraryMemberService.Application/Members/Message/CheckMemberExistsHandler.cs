using System;
using System.Threading.Tasks;
using LibraryMessaging.Contracts.Members;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace LibraryMemberService.Members.Message;

public class CheckMemberExistsHandler :
    IHandleMessages<CheckMemberExistsRequest>,
    ITransientDependency
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IBus _bus;
    private readonly ILogger<CheckMemberExistsHandler> _logger;

    public CheckMemberExistsHandler(
        IServiceScopeFactory scopeFactory,
        IBus bus,
        ILogger<CheckMemberExistsHandler> logger)
    {
        _scopeFactory = scopeFactory;
        _bus = bus;
        _logger = logger;
    }

    public async Task Handle(CheckMemberExistsRequest message)
    {
        _logger.LogInformation(
            "Received member check request. MemberId: {MemberId}, CorrelationId: {CorrelationId}",
            message.MemberId, message.CorrelationId);

        try
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
                        member => member.Id == message.MemberId);

                    await uow.CompleteAsync();
                }
            }

            _logger.LogInformation(
                "Member database check completed. MemberId: {MemberId}, Exists: {Exists}, CorrelationId: {CorrelationId}",
                message.MemberId, exists, message.CorrelationId);

            await _bus.Send(new CheckMemberExistsResponse
            {
                CorrelationId = message.CorrelationId,
                MemberId = message.MemberId,
                Exists = exists
            });

            _logger.LogInformation(
                "Member check result sent to Kafka. CorrelationId: {CorrelationId}",
                message.CorrelationId);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error processing member check request. MemberId: {MemberId}, CorrelationId: {CorrelationId}",
                message.MemberId, message.CorrelationId);

            throw;
        }
    }
}