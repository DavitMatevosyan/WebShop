namespace Catalog.Domain.Entities;

//get/list/add/update/delete.
public sealed class Product : BaseEntity
{
    public required string Name { get; set; } // max 50 chars
    public string? Description { get; set; }
    public string? Image { get; set; }
    public Guid CategoryId { get; set; }
    public required decimal Price { get; set; }
    public required int Amount { get; set; } // > 0

    public required Category Category { get; set; } // 1 product many categories
}