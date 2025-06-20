using Catalog.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.DomainEventHandlers;

public class CategoryImageChangedEventHandler(ILogger<CategoryImageChangedEventHandler> logger) : INotificationHandler<CategoryImageChangedEvent>
{
    public Task Handle(CategoryImageChangedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Category image changed: Id = {notification.Id}");
        return Task.CompletedTask;
    }
}