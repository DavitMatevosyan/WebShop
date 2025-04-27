using Catalog.Application.Categories.Commands;
using MediatR;

namespace Catalog.Api.Endpoints.Category;

public class DeleteCategoryEndpoint(Mediator mediator) : BaseEndpoint(mediator)
{
    public async Task<IResult> HandleAsync(Guid id)
    {
        var request = new DeleteCategoryCommand(id);
        
        var result = await Mediator.Send(request);
        
        return result == default ? Results.Problem("Category failed to delete") 
                                 : Results.Redirect("/", true);
    }
}