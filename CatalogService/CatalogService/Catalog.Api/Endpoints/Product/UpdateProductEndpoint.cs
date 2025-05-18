using Catalog.Application.Products.Commands;
using Catalog.Application.Products.Dtos;
using MediatR;

namespace Catalog.Api.Endpoints.Product;

public class UpdateProductEndpoint(IMediator mediator) : BaseEndpoint(mediator)
{
    public async Task<IResult> HandleAsync(UpdateProductDto dto)
    {
        var command = new UpdateProductCommand(
            dto.Id, 
            dto.Name,
            dto.Description,
            dto.Image,
            dto.CategoryId,
            dto.Price,
            dto.Amount);

        var result = await Mediator.Send(command);
        
        if(result == Guid.Empty)
            return Results.Problem("Product failed to update", statusCode: 400);

        var responseDto = new UpdateProductResult(
            result,
            dto.Name,
            dto.Description,
            dto.Image,
            dto.CategoryId,
            dto.Price,
            dto.Amount);
        
        return Results.Ok(responseDto);
    }
}

public record UpdateProductResult(
    Guid Id,
    string? Name = null, 
    string? Description = null,
    string? Image = null,
    Guid? CategoryId = null,
    decimal? Price = null,
    int? Amount = null);