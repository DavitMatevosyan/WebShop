using Catalog.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.DomainEventHandlers;

public class CategoryNameChangedEventHandler(ILogger<CategoryNameChangedEventHandler> logger) : INotificationHandler<CategoryNameChangedEvent>
{
    public Task Handle(CategoryNameChangedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Category name changed: Id = {notification.Id}, New Name = {notification.Name}");
        return Task.CompletedTask;
    }
}