using Catalog.Domain.DomainEvents;
using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Catalog.Domain.ValueObjects;
using FluentAssertions;

namespace Catalog.Domain.Tests.DomainEvents;

// Similarly add all domain event tests with success and failure cases for category also
public class ProductDomainEventTests
{
    [Fact]
    public void ChangeName_NewName_ShouldChangeName()
    {
        Product product = new Product(
            "Product name", 
            "Product Description",
            "Image URI",
            Guid.NewGuid(),
            new Money(15),
            5);
        
        product.ChangeName("New Name");
        
        var domainEvent = product.DomainEvents.OfType<ProductNameChangedEvent>().FirstOrDefault();

        Assert.NotNull(domainEvent);
        Assert.Equal("New Name", domainEvent.Name);
    }
    
    [Fact]
    public void ChangeName_InvalidName_ShouldChangeName()
    {
        Product product = new Product(
            "Product name", 
            "Product Description",
            "Image URI",
            Guid.NewGuid(),
            new Money(15),
            5);
        
        var act = () => product.ChangeName("A very very long product name which exceeds 50 characters");
        
        act.Should().Throw<DomainException>().WithMessage("Product name cannot exceed 50 chars");
        
        Assert.Equal("Product name", product.Name);
    }
}