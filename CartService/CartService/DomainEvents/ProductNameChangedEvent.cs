namespace CartService.DomainEvents;

public class ProductNameChangedEvent : BaseDomainEvent
{
    public required string Name { get; init; }
}