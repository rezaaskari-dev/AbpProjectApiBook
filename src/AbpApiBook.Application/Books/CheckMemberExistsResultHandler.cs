
using System.Threading.Tasks;
using LibraryMessaging.Contracts.Members;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace AbpApiBook.Books;

public class CheckMemberExistsResultHandler :
    IDistributedEventHandler<CheckMemberExistsResultEto>,
    ITransientDependency
{
    private readonly MemberCheckResponseManager _responseManager;

    public CheckMemberExistsResultHandler(
        MemberCheckResponseManager responseManager)
    {
        _responseManager = responseManager;
    }

    public Task HandleEventAsync(CheckMemberExistsResultEto eventData)
    {
        _responseManager.SetResponse(
            eventData.CorrelationId,
            eventData.Exists);

        return Task.CompletedTask;
    }
}
