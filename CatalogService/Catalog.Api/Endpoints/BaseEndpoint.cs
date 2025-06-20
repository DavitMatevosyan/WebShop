using MediatR;

namespace Catalog.Api.Endpoints;

public abstract class BaseEndpoint(IMediator mediator)
{
    protected readonly IMediator Mediator = mediator;
}