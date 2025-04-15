using System.Threading;
using System.Threading.Tasks;

namespace RemainingMediator;

public interface IPublisher
{
    Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification;
}
