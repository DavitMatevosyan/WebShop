using Catalog.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.DomainEventHandlers;

public class ProductAmountChangedEventHandler(ILogger<ProductAmountChangedEventHandler> logger) : INotificationHandler<ProductAmountChangedEvent>
{
    public Task Handle(ProductAmountChangedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Product amount changed: Id = {notification.Id}, New Amount = {notification.Amount}");
        return Task.CompletedTask;
    }
}