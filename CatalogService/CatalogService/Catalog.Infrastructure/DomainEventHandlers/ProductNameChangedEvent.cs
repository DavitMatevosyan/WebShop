using Catalog.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.DomainEventHandlers;

public class ProductNameChangedEventHandler(ILogger<ProductNameChangedEventHandler> logger) : INotificationHandler<ProductNameChangedEvent>
{
    public Task Handle(ProductNameChangedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Product name changed: Id = {notification.Id}, New Name = {notification.Name}");
        return Task.CompletedTask;
    }
}