using System.Threading;
using System.Threading.Tasks;

namespace OpenMediator;

public interface IPublisher
{
    Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification;
}
