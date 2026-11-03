using System;
using System.Threading.Tasks;
using LibraryMessaging.Contracts.Members;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Volo.Abp.DependencyInjection;

namespace AbpApiBook.Books;

public class MemberCheckService : ITransientDependency
{
    private readonly IBus _bus;
    private readonly MemberCheckResponseManager _responseManager;
    private readonly ILogger<MemberCheckService> _logger;

    public MemberCheckService(
        IBus bus,
        MemberCheckResponseManager responseManager,
        ILogger<MemberCheckService> logger)
    {
        _bus = bus;
        _responseManager = responseManager;
        _logger = logger;
    }

    public async Task<bool> CheckMemberAsync(Guid memberId)
    {
        var correlationId = Guid.NewGuid().ToString();

        _logger.LogInformation(
            "Starting member check. MemberId: {MemberId}, CorrelationId: {CorrelationId}",
            memberId, correlationId);

        var responseTask = _responseManager.WaitForResponseAsync(
            correlationId,
            TimeSpan.FromSeconds(15));

        try
        {
            _logger.LogInformation(
                "Sending member check message to Kafka. CorrelationId: {CorrelationId}",
                correlationId);

            await _bus.Send(new CheckMemberExistsEto
            {
                MemberId = memberId,
                CorrelationId = correlationId
            });

            _logger.LogInformation(
                "Member check message sent. CorrelationId: {CorrelationId}",
                correlationId);

            var result = await responseTask;

            _logger.LogInformation(
                "Member check response received. MemberId: {MemberId}, Exists: {Exists}, CorrelationId: {CorrelationId}",
                memberId, result, correlationId);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Member check failed. MemberId: {MemberId}, CorrelationId: {CorrelationId}",
                memberId, correlationId);

            throw;
        }
    }
}