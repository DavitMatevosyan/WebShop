using Catalog.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.DomainEventHandlers;

public class ProductDescriptionChangedEventHandler(ILogger<ProductDescriptionChangedEventHandler> logger) : INotificationHandler<ProductDescriptionChangedEvent>
{
    public Task Handle(ProductDescriptionChangedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Product description changed: Id = {notification.Id}");
        return Task.CompletedTask;
    }
}