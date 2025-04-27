using Catalog.Application.Categories.Queries;
using MediatR;

namespace Catalog.Api.Endpoints.Category;

public class GetCategoriesEndpoint(Mediator mediator) : BaseEndpoint(mediator)
{
    public async Task<IResult> HandleAsync(string? searchText, int page, int pageSize)
    {
        var query = new GetCategoriesQuery(searchText, page, pageSize);
        
        var result = await Mediator.Send(query);

        return !result.Any() ? Results.NotFound() : Results.Ok(result);
    }
}