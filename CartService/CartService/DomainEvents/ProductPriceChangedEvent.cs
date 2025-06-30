namespace CartService.DomainEvents;

public class ProductPriceChangedEvent : BaseDomainEvent
{
	public required decimal Price { get; init; }
}