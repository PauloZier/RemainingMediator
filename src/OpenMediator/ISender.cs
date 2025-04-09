using System.Threading;
using System.Threading.Tasks;

namespace OpenMediator;

public interface ISender
{
    Task SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest;
    Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>;
}
