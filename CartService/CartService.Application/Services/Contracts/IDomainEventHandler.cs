using CartService.Application.DomainEvents;

namespace CartService.Application.Services.Contracts;

public interface IDomainEventHandler
{
	Task HandleAsync(BaseDomainEvent domainEvent);
}