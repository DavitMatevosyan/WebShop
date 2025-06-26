namespace CartService.Application.DomainEvents;

public class ProductNameChangedEvent : BaseDomainEvent
{
    public required string Name { get; init; }
}