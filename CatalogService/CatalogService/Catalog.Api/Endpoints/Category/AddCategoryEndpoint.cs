using Catalog.Application.Categories.Commands;
using Catalog.Application.Categories.Dtos;
using MediatR;

namespace Catalog.Api.Endpoints.Category;

public class AddCategoryEndpoint(IMediator mediator) : BaseEndpoint(mediator)
{
    public async Task<IResult> HandleAsync(AddCategoryDto categoryDto)
    {
        var command = new AddCategoryCommand(categoryDto.Name, categoryDto.Image, categoryDto.ParentCategoryId);
        
        var result = await Mediator.Send(command);
        
        if(result == Guid.Empty)
            return Results.Problem("Category failed to add", statusCode: 400);
        
        var dto = new AddCategoryResponse(result, command.Name, command.Image, command.ParentCategoryId);

        return Results.Ok(dto);
    }
}

public record AddCategoryResponse(Guid Id, string Name, string? Image, Guid? ParentCategoryId);