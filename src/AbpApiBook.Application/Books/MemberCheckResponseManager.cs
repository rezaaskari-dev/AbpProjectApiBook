using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace AbpApiBook.Books;

public class MemberCheckResponseManager : ISingletonDependency
{
    private readonly ConcurrentDictionary<
        string,
        TaskCompletionSource<bool>> _pendingRequests = new();

    private readonly ILogger<MemberCheckResponseManager> _logger;

    public MemberCheckResponseManager(
        ILogger<MemberCheckResponseManager> logger)
    {
        _logger = logger;
    }

    public async Task<bool> WaitForResponseAsync(
        string correlationId,
        TimeSpan timeout)
    {
        var tcs = new TaskCompletionSource<bool>(
            TaskCreationOptions.RunContinuationsAsynchronously);

        if (!_pendingRequests.TryAdd(correlationId, tcs))
        {
            throw new InvalidOperationException(
                $"Duplicate CorrelationId: {correlationId}");
        }

        _logger.LogWarning(
            "WAIT STARTED. CorrelationId: {CorrelationId}",
            correlationId);

        try
        {
            var completedTask = await Task.WhenAny(
                tcs.Task,
                Task.Delay(timeout));

            if (completedTask != tcs.Task)
            {
                _logger.LogError(
                    "WAIT TIMEOUT. CorrelationId: {CorrelationId}",
                    correlationId);

                throw new TimeoutException(
                    $"No response received for {correlationId}");
            }

            var result = await tcs.Task;

            _logger.LogWarning(
                "WAIT COMPLETED. CorrelationId: {CorrelationId}, Exists: {Exists}",
                correlationId,
                result);

            return result;
        }
        finally
        {
            _pendingRequests.TryRemove(correlationId, out _);
        }
    }

    public void SetResponse(
        string correlationId,
        bool exists)
    {
        _logger.LogWarning(
            "RESPONSE RECEIVED. CorrelationId: {CorrelationId}, Exists: {Exists}",
            correlationId,
            exists);

        if (_pendingRequests.TryGetValue(correlationId, out var tcs))
        {
            var completed = tcs.TrySetResult(exists);

            _logger.LogWarning(
                "TRY SET RESULT: {Completed}, CorrelationId: {CorrelationId}",
                completed,
                correlationId);
        }
        else
        {
            _logger.LogError(
                "NO PENDING REQUEST FOUND! CorrelationId: {CorrelationId}",
                correlationId);
        }
    }
}