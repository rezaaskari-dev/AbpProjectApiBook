using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AbpApiBook.Books
{
    public class MemberCheckResponseManager : ISingletonDependency
    {
        private readonly ConcurrentDictionary<
            string,
            TaskCompletionSource<bool>> _pendingRequests = new();

        public Task<bool> WaitForResponseAsync(
            string correlationId,
            TimeSpan timeout)
        {
            var tcs = new TaskCompletionSource<bool>(
                TaskCreationOptions.RunContinuationsAsynchronously);

            _pendingRequests[correlationId] = tcs;

            return WaitWithTimeoutAsync(
                correlationId,
                tcs,
                timeout);
        }

        public void SetResponse(
            string correlationId,
            bool exists)
        {
            if (_pendingRequests.TryRemove(
                correlationId,
                out var tcs))
            {
                tcs.TrySetResult(exists);
            }
        }

        private async Task<bool> WaitWithTimeoutAsync(
            string correlationId,
            TaskCompletionSource<bool> tcs,
            TimeSpan timeout)
        {
            var completedTask = await Task.WhenAny(
                tcs.Task,
                Task.Delay(timeout));

            if (completedTask == tcs.Task)
            {
                return await tcs.Task;
            }

            _pendingRequests.TryRemove(
                correlationId,
                out _);

            throw new TimeoutException(
                $"No response received for member check. " +
                $"CorrelationId: {correlationId}");
        }
    }

}