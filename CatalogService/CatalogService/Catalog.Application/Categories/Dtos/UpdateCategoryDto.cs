namespace Catalog.Application.Categories.Dtos;

public record UpdateCategoryDto(
    Guid Id,
    string Name,
    string? Image,
    Guid ParentCategoryId);