using Catalog.Application.Categories.Dtos;
using Catalog.Application.Extensions.Mappers;
using Catalog.Domain.Contracts;
using Catalog.Domain.Entities;
using LinqKit;
using MediatR;

namespace Catalog.Application.Categories.Queries;

public record GetCategoriesQuery(string? SearchText) : IRequest<ICollection<CategoryDto>>;

public class GetCategoriesQueryHandler(ICategoryRepository repository) : IRequestHandler<GetCategoriesQuery, ICollection<CategoryDto>>
{
    public async Task<ICollection<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Category>(true);

        if (request.SearchText is not null)
            predicate = predicate.And(product => product.Name.Contains(request.SearchText));
        
        var result = await repository.GetAsync(predicate);

        return result.Select(category => category.ToDto()).ToList();
    }
}