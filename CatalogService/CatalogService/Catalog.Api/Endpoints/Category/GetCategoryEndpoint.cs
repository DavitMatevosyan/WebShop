using Catalog.Application.Categories.Queries;
using MediatR;

namespace Catalog.Api.Endpoints.Category;


public class GetCategoryEndpoint(IMediator mediator) : BaseEndpoint(mediator)
{
    public async Task<IResult> HandleAsync(Guid id)
    {
        var query = new GetCategoryQuery(id);
    
        var result = await Mediator.Send(query);

        return Results.Ok(result);
    }
}