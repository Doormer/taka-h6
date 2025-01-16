using MediatR;
using Polly;
using Microsoft.Extensions.Logging;

namespace Chat.Application.Behaviors;

public class RetryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<RetryBehavior<TRequest, TResponse>> _logger;
    private readonly int _retryCount = 3;

    public RetryBehavior(ILogger<RetryBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var retryPolicy = Policy<TResponse>
            .Handle<Exception>()
            .WaitAndRetryAsync(
                _retryCount,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, _, attemptNumber, _) =>
                {
                    var requestName = typeof(TRequest).Name;
                    _logger.LogWarning(
                        "Request {Name} failed with error {Error}. Retry attempt {Attempt}",
                        requestName,
                        exception.ToString(),
                        attemptNumber);
                });

        return await retryPolicy.ExecuteAsync(async () => await next());
    }
} 