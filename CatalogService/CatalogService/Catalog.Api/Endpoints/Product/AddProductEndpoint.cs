using Catalog.Application.Products.Commands;
using Catalog.Application.Products.Dtos;
using MediatR;

namespace Catalog.Api.Endpoints.Product;

public record AddProductResponse(
    Guid Id, 
    string Name, 
    string Description,
    string Image,
    Guid CategoryId,
    decimal Price,
    int Amount);

public class AddProductEndpoint(Mediator mediator) : BaseEndpoint(mediator)
{
    public async Task<IResult> HandleAsync(AddProductDto categoryDto)
    {
        var command = new AddProductCommand(
            categoryDto.Name,
            categoryDto.Description ?? string.Empty,
            categoryDto.Image ?? string.Empty,
            categoryDto.CategoryId,
            categoryDto.Price,
            categoryDto.Amount);
        
        var result = await Mediator.Send(command);
        
        if(result == Guid.Empty)
            return Results.Problem("Product failed to add", statusCode: 400);
        
        var dto = new AddProductResponse(
            result, 
            command.Name,
            command.Description,
            command.Image, 
            command.CategoryId,
            command.Price,
            command.Amount);

        return Results.Ok(dto);
    }
}