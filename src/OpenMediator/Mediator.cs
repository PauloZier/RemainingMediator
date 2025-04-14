using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OpenMediator;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default) 
        where TNotification : INotification
    {
        var handlers = _serviceProvider.GetServices<INotificationHandler<TNotification>>() ?? [];
        var tasks = handlers.Select(x => x.HandleAsync(notification, cancellationToken));

        return Task.WhenAll(tasks);
    }

    public Task SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) 
        where TRequest : IRequest
    {
        var handler = _serviceProvider.GetRequiredService<IRequestHandler<TRequest>>();
        return handler.HandleAsync(request, cancellationToken);
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));

        var handlerObj = _serviceProvider.GetService(handlerType);
        if (handlerObj == null)
            throw new InvalidOperationException($"Handler not found for {requestType.Name}");

        var method = handlerType.GetMethod("HandleAsync");
        if (method == null)
            throw new InvalidOperationException($"HandleAsync method not found in {handlerType.Name}");

        if (method.Invoke(handlerObj, [request, cancellationToken]) is not Task<TResponse> task)
            throw new InvalidOperationException(
                $"Handler {handlerObj.GetType().Name} returned null or an incompatible type.");

        return await task;
    }
}