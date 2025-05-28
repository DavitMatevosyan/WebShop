using Catalog.Application.Categories.Commands;
using Catalog.Application.Categories.Dtos;
using MediatR;

namespace Catalog.Api.Endpoints.Category;

public record UpdateCategoryResult(Guid Id, string? Name, string? Image, Guid? ParentCategoryId);

public class UpdateCategoryEndpoint(IMediator mediator) : BaseEndpoint(mediator)
{
    public async Task<IResult> HandleAsync(UpdateCategoryDto dto)
    {
        UpdateCategoryCommand command = new UpdateCategoryCommand(dto.Id, dto.Name, dto.Image, dto.ParentCategoryId);

        var result = await Mediator.Send(command);
        
        if(result == Guid.Empty)
            return Results.Problem("Category failed to update", statusCode: 400);

        var responseDto = new UpdateCategoryResult(result, dto.Name, dto.Image, dto.ParentCategoryId);
        
        return Results.Ok(responseDto);
    }
}