using Catalog.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.DomainEventHandlers;

public class ParentCategoryIdChangedEventHandler(ILogger<ParentCategoryIdChangedEventHandler> logger) : INotificationHandler<ParentCategoryIdChangedEvent>
{
    public Task Handle(ParentCategoryIdChangedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Parent category changed: Id = {notification.Id}, New ParentCategoryId = {notification.ParentCategoryId}");
        return Task.CompletedTask;
    }
}