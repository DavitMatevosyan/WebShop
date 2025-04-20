using Catalog.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.DomainEventHandlers;

public class CategoryDeletedEventHandler(ILogger<CategoryDeletedEventHandler> logger) : INotificationHandler<CategoryDeletedEvent>
{
    public Task Handle(CategoryDeletedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Category deleted: Id = {notification.Id}");
        return Task.CompletedTask;
    }
}