namespace Catalog.Application.Products.Dtos;

public record AddProductDto(
    string Name, 
    string? Description, 
    string? Image, 
    Guid CategoryId, 
    decimal Price, 
    int Amount);