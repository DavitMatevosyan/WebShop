using Catalog.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.DomainEventHandlers;

public class ProductImageChangedEventHandler(ILogger<ProductImageChangedEventHandler> logger) : INotificationHandler<ProductImageChangedEvent>
{
    public Task Handle(ProductImageChangedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Product image changed: Id = {notification.Id}");
        return Task.CompletedTask;
    }
}