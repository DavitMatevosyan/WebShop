namespace CartService.Application.DomainEvents;

public class ProductPriceChangedEvent : BaseDomainEvent
{
	public required decimal Price { get; init; }
}