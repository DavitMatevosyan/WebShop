using Catalog.Domain.Contracts;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Categories.Commands;

public record AddCategoryCommand(string Name, string Image, Guid? ParentCategoryId) : IRequest<Guid>;

public class AddCategoryCommandHandler(ICategoryRepository repository) : IRequestHandler<AddCategoryCommand, Guid>
{
    public async Task<Guid> Handle(AddCategoryCommand request, CancellationToken cancellationToken) // cancellation token not used currently, but can be used.
    {
        var category = new Category(request.Name, request.Image, request.ParentCategoryId);

        await repository.AddAsync(category);

        return category.Id;
    }
}