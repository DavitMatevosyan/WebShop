namespace Catalog.Application.Categories.Dtos;

public record AddCategoryDto(
    string Name,
    string? Image,
    Guid ParentCategoryId);