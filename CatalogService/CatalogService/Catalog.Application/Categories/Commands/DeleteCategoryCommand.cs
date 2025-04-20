using Catalog.Domain.Contracts;
using Catalog.Domain.Exceptions;
using MediatR;

namespace Catalog.Application.Categories.Commands;

public record DeleteCategoryCommand(Guid Id) : IRequest<Guid>;

public class DeleteCategoryCommandHandler(ICategoryRepository repository) : IRequestHandler<DeleteCategoryCommand, Guid>
{
    public async Task<Guid> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetAsync(request.Id)
            ?? throw new NotFoundException("The category does not exist");

        category.DeleteCategory();

        await repository.UpdateAsync(category);
        
        return category.Id;
    }
}