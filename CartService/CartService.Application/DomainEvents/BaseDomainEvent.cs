namespace CartService.Application.DomainEvents;

public abstract class BaseDomainEvent
{
    public EventType EventType { get; init; }
    public Guid Id { get; init; }
}