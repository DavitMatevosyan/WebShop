namespace Catalog.Application.Products.Dtos;

public record UpdateProductDto(
    Guid Id, 
    string Name, 
    string? Description, 
    string? Image, 
    Guid CategoryId, 
    decimal Price, 
    int Amount);