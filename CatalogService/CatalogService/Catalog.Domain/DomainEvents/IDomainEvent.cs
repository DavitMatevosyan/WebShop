using Catalog.Domain.ValueObjects;
using MediatR;

namespace Catalog.Domain.DomainEvents;

public interface IDomainEvent : INotification;

// Product Domain Events
public record ProductNameChangedEvent(Guid Id, string Name) : IDomainEvent;
public record ProductDescriptionChangedEvent(Guid Id, string Description) : IDomainEvent;
public record ProductImageChangedEvent(Guid Id, string Image) : IDomainEvent;
public record ProductCategoryIdChangedEvent(Guid Id, Guid CategoryId) : IDomainEvent;
public record ProductPriceChangedEvent(Guid Id, Money Price) : IDomainEvent;
public record ProductAmountChangedEvent(Guid Id, int Amount) : IDomainEvent;
public record ProductDeletedEvent(Guid Id) : IDomainEvent;

// Category Domain Events
public record CategoryNameChangedEvent(Guid Id, string Name) : IDomainEvent;
public record CategoryImageChangedEvent(Guid Id, string Image) : IDomainEvent;
public record ParentCategoryIdChangedEvent(Guid Id, Guid ParentCategoryId) : IDomainEvent;
public record CategoryDeletedEvent(Guid Id) : IDomainEvent;

