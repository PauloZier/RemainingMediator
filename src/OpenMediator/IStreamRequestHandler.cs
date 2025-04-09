using System.Collections.Generic;
using System.Threading;

namespace OpenMediator;

public interface IStreamRequestHandler<in TRequest, out TResponse> where TRequest : IStreamRequest<TResponse>
{
    IAsyncEnumerable<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
