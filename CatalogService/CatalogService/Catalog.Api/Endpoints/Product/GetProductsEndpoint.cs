using Catalog.Application.Products.Queries;
using MediatR;

namespace Catalog.Api.Endpoints.Product;

public class GetProductsEndpoint(Mediator mediator) : BaseEndpoint(mediator)
{
    // the filtration is only done by category Id, just because currently that is required, but it is possible to extend it easily
    public async Task<IResult> HandleAsync(Guid categoryId, int page, int pageSize)
    {
        var query = new GetProductsQuery(
            page,
            pageSize,
            CategoryId: categoryId);
        
        var result = await Mediator.Send(query);

        return !result.Any() ? Results.NotFound() : Results.Ok(result);
    }
}