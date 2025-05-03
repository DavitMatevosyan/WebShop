using Catalog.Domain.DomainEvents;
using Catalog.Domain.Exceptions;

namespace Catalog.Domain.Entities;

public class Category(string name, string? image, Guid? parentCategoryId) : AggregateRoot
{
    public string Name { get; private set; } = name;
    public string? Image { get; private set; } = image;
    public Guid? ParentCategoryId { get; private set; } = parentCategoryId;

    public Category? ParentCategory { get; private set; }
    public List<Product> Products { get; private set; } = [];
    
    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Category name cannot be empty");

        if(name.Length > 50)
            throw new DomainException("Category name cannot exceed 50 chars");
            
        if (Name == name) 
            return;
        
        Name = name;
        AddDomainEvent(new CategoryNameChangedEvent(Id, name));
    }
    
    public void ChangeImage(string image)
    {
        if (Image == image) 
            return;
        
        Image = image;
        AddDomainEvent(new CategoryImageChangedEvent(Id, Image));
    }
    
    public void ChangeParentCategoryId(Guid categoryId)
    {
        if (parentCategoryId == categoryId) 
            return;
        
        ParentCategoryId = categoryId;
        AddDomainEvent(new ParentCategoryIdChangedEvent(Id, categoryId));
    }
    
    public void DeleteCategory()
    {
        if (IsDeleted)
            throw new DomainException("Category is already deleted");

        IsDeleted = true;
        
        AddDomainEvent(new CategoryDeletedEvent(Id));
    }
}