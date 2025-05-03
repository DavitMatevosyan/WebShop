using System.Linq.Expressions;
using Catalog.Application.Extensions.Mappers;
using Catalog.Application.Products.Dtos;
using Catalog.Domain.Contracts;
using Catalog.Domain.Entities;
using LinqKit;
using MediatR;

namespace Catalog.Application.Products.Queries;

public record GetProductsQuery(string? SearchText, decimal? MinPrice, decimal? MaxPrice, int? MinAmount, int? MaxAmount) : IRequest<ICollection<ProductDto>>;

public class GetProductsQueryHandler(IProductRepository repository) : IRequestHandler<GetProductsQuery, ICollection<ProductDto>>
{
    public async Task<ICollection<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Product>(true);

        if (request.SearchText is not null)
            predicate = predicate.And(product => product.Name.Contains(request.SearchText) || (product.Description != null && product.Description.Contains(request.SearchText)));

        if (request.MinPrice is not null)
            predicate = predicate.And(product => product.Price.Value >= request.MinPrice);

        if (request.MaxPrice is not null)
            predicate = predicate.And(product => product.Price.Value <= request.MaxPrice);

        if (request.MinAmount is not null)
            predicate = predicate.And(product => product.Amount >= request.MinAmount);

        if (request.MaxAmount is not null)
            predicate = predicate.And(product => product.Amount <= request.MaxAmount);

        var result = await repository.GetAsync(predicate);

        return result.Select(product => product.ToDto()).ToList();
    }
}