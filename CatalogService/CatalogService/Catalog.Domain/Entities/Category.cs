namespace Catalog.Domain.Entities;

// get/list/add/update/delete
public sealed class Category : BaseEntity
{
    public required string Name { get; set; } // set max length 50 ef fluent
    public string? Image { get; set; }
    public Category? ParentCategory { get; set; } = null;
}