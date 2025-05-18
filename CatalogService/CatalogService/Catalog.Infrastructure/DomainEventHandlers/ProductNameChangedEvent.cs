using Catalog.Domain.DomainEvents;
using Catalog.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.DomainEventHandlers;

public class ProductNameChangedEventHandler(
    RabbitMqService rabbitMqService,
    ILogger<ProductNameChangedEventHandler> logger) : INotificationHandler<ProductNameChangedEvent>
{
    public async Task Handle(ProductNameChangedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Product name changed: Id = {notification.Id}, New Name = {notification.Name}");
        
        await rabbitMqService.Publish(Constants.ExchangeNames.DomainEventExchange, notification, cancellationToken);
    }
}