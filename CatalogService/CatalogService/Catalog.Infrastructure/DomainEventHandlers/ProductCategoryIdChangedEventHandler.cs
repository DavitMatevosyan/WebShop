using Catalog.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.DomainEventHandlers;

public class ProductCategoryIdChangedEventHandler(ILogger<ProductCategoryIdChangedEventHandler> logger) : INotificationHandler<ProductCategoryIdChangedEvent>
{
    public Task Handle(ProductCategoryIdChangedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Product category changed: Id = {notification.Id}, New CategoryId = {notification.CategoryId}");
        return Task.CompletedTask;
    }
}