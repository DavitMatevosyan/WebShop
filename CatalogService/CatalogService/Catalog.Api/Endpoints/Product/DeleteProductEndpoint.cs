using Catalog.Application.Products.Commands;
using MediatR;

namespace Catalog.Api.Endpoints.Product;

public class DeleteProductEndpoint(IMediator mediator) : BaseEndpoint(mediator)
{
    public async Task<IResult> HandleAsync(Guid id)
    {
        var request = new DeleteProductCommand(id);
        
        var result = await Mediator.Send(request);
        
        return result == default ? Results.Problem("Product failed to delete") 
                                 : Results.Redirect("/", true);
    }
}