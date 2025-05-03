using Catalog.Application.Categories.Dtos;
using Catalog.Domain.ValueObjects;

namespace Catalog.Application.Products.Dtos;

public record ProductDto(Guid Id, string Name, string? Description, string? Image, CategoryDto Category, Money Price, int Amount);