using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OpenMediator;

public class MediatorHandler : IMediatorHandler
{
    private readonly IServiceProvider _serviceProvider;

    public MediatorHandler(IServiceProvider serviceProvider)
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

    public async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) 
        where TRequest : IRequest<TResponse>
    {
        var handler = _serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        return await handler.HandleAsync(request, cancellationToken);
    }

}