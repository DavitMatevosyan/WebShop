using Catalog.Domain.Entities;

namespace Catalog.Application.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchEventAsync(IEnumerable<AggregateRoot> entities);
}