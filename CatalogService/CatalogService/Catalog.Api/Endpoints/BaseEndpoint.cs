using MediatR;

namespace Catalog.Api.Endpoints;

public abstract class BaseEndpoint(Mediator mediator)
{
    protected readonly Mediator Mediator = mediator;
}