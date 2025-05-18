using Catalog.Domain.DomainEvents;
using Catalog.Domain.Exceptions;
using Catalog.Domain.ValueObjects;

namespace Catalog.Domain.Entities;

public class Product(string name, string? description, string? image, Guid categoryId, Money price, int amount)
    : AggregateRoot
{
    public string Name { get; private set; } = name;
    public string? Description { get; private set; } = description;
    public string? Image { get; private set; } = image;
    public Guid CategoryId { get; private set; } = categoryId;
    public Money Price { get; private set; } = price;
    public int Amount { get; private set; } = amount;

    public Category Category { get; set; } = null!;

    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name cannot be empty");

        if(name.Length > 50)
            throw new DomainException("Product name cannot exceed 50 chars");
            
        if (Name == name) 
            return;
        
        Name = name;
        AddDomainEvent(new ProductNameChangedEvent(Id, name));
    }
    
    public void ChangeDescription(string description)
    {
        if (Description == description) 
            return;
        
        Description = description;
        AddDomainEvent(new ProductDescriptionChangedEvent(Id, Description));
    }
    
    public void ChangeImage(string image)
    {
        if (Image == image) 
            return;
        
        Image = image;
        AddDomainEvent(new ProductImageChangedEvent(Id, Image));
    }
    
    public void ChangeCategoryId(Guid categoryId)
    {
        if (CategoryId == categoryId) 
            return;
        
        CategoryId = categoryId;
        AddDomainEvent(new ProductCategoryIdChangedEvent(Id, CategoryId));
    }
    
    public void ChangePrice(decimal newPrice)
    {
        Price = new Money(newPrice);

        AddDomainEvent(new ProductPriceChangedEvent(Id, Price));
    }
    
    public void ChangeAmount(int amount)
    {
        if(amount < 0)
            throw new DomainException("Amount cannot be negative");
        
        if (Amount == amount) 
            return;
        
        Amount = amount;
        AddDomainEvent(new ProductAmountChangedEvent(Id, Amount));
    }

    public void DeleteProduct()
    {
        if (IsDeleted)
            throw new DomainException("Product is already deleted");

        IsDeleted = true;
        
        AddDomainEvent(new ProductDeletedEvent(Id));
    }
}