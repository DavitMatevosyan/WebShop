namespace Catalog.Application.Interfaces;

public interface INotificationPublisher
{
    Task Publish<T>(string exchange, T message, CancellationToken cancellationToken = default);
}