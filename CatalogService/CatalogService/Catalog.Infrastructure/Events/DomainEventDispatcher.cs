using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Infrastructure.Events;

public class DomainEventDispatcher(IMediator mediator) : IDomainEventDispatcher
{
    public async Task DispatchEventAsync(IEnumerable<AggregateRoot> entities)
    {
        foreach (var entity in entities)
        {
            var events = entity.DomainEvents.ToList();
            
            entity.ClearDomainEvents();

            foreach (var domainEvent in events)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
}