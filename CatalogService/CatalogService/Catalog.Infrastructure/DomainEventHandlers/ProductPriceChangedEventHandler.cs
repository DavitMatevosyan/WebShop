using Catalog.Domain.DomainEvents;
using Catalog.Infrastructure.Constants;
using MediatR;
using Microsoft.Extensions.Logging;
using INotificationPublisher = Catalog.Application.Interfaces.INotificationPublisher;

namespace Catalog.Infrastructure.DomainEventHandlers;

public class ProductPriceChangedEventHandler(INotificationPublisher publisher, ILogger<ProductPriceChangedEventHandler> logger) : INotificationHandler<ProductPriceChangedEvent>
{
    public async Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
    {
        await publisher.Publish(ExchangeNames.DomainEventExchange, notification, cancellationToken);
        
        logger.LogInformation($"Product price changed: Id = {notification.Id}, New Price = {notification.Price.Value}");
    }
}