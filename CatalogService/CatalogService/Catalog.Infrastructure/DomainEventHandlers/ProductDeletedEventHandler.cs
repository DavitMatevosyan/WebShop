using Catalog.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.DomainEventHandlers;

public class ProductDeletedEventHandler(ILogger<ProductDeletedEventHandler> logger) : INotificationHandler<ProductDeletedEvent>
{
    public Task Handle(ProductDeletedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Product deleted: Id = {notification.Id}");
        return Task.CompletedTask;
    }
}