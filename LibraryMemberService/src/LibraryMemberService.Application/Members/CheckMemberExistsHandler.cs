using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace LibraryMemberService.Members
{
    public class CheckMemberExistsHandler :
     IDistributedEventHandler<CheckMemberExistsEto>,
     ITransientDependency
    {
        public async Task HandleEventAsync(CheckMemberExistsEto eventData)
        {
            Console.WriteLine(
                $"[RabbitMQ] CheckMemberExists received. " +
                $"MemberId: {eventData.MemberId}, " +
                $"CorrelationId: {eventData.CorrelationId}");

            await Task.CompletedTask;
        }
    }
}