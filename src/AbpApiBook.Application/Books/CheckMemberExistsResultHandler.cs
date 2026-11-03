using System.Threading.Tasks;
using LibraryMessaging.Contracts.Members;
using Microsoft.Extensions.Logging;
using Rebus.Handlers;
using Volo.Abp.DependencyInjection;

namespace AbpApiBook.Books;

public class CheckMemberExistsResultHandler :
    IHandleMessages<CheckMemberExistsResultEto>,
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

    public Task Handle(CheckMemberExistsResultEto message)
    {
        _logger.LogInformation(
            "Received member check result from Kafka. MemberId: {MemberId}, Exists: {Exists}, CorrelationId: {CorrelationId}",
            message.MemberId,
            message.Exists,
            message.CorrelationId);

        _responseManager.SetResponse(
            message.CorrelationId,
            message.Exists);

        _logger.LogInformation(
            "Member check response delivered to waiting request. CorrelationId: {CorrelationId}",
            message.CorrelationId);

        return Task.CompletedTask;
    }
}