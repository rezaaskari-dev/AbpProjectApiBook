using System.Threading.Tasks;
using LibraryMessaging.Contracts.Members;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace AbpApiBook.Books;

public class CheckMemberExistsResultHandler :
    IDistributedEventHandler<CheckMemberExistsResultEto>,
    ITransientDependency
{
    private readonly MemberCheckResponseManager _responseManager;
    private readonly ILogger<CheckMemberExistsResultHandler> _logger;

    public CheckMemberExistsResultHandler(
        MemberCheckResponseManager responseManager,
        ILogger<CheckMemberExistsResultHandler> logger)
    {
        _responseManager = responseManager;
        _logger = logger;
    }

    public Task HandleEventAsync(CheckMemberExistsResultEto eventData)
    {
        _logger.LogWarning(
            "RESULT HANDLER EXECUTED. CorrelationId: {CorrelationId}, MemberId: {MemberId}, Exists: {Exists}",
            eventData.CorrelationId,
            eventData.MemberId,
            eventData.Exists);

        _responseManager.SetResponse(
            eventData.CorrelationId,
            eventData.Exists);

        return Task.CompletedTask;
    }
}