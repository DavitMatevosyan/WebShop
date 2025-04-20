namespace Catalog.Application.Categories.Dtos;

public record CategoryDto(
    Guid Id,
    string Name,
    string? Image,
    Guid? ParentCategoryId,
    string? ParentCategoryName,
    string? ParentCategoryImage);