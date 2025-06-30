using CartService.DomainEvents;

namespace CartService.Services.Contracts;

public interface IDomainEventHandler
{
	Task HandleAsync(BaseDomainEvent domainEvent);
}