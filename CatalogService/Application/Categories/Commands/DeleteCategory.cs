using Application.Common.Identity;
using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Identity;
using MediatR;

namespace Application.Categories.Commands;

[Authorize(Roles.Manager)]
public record DeleteCategoryCommand(int id) : IRequest;

public class DeleteCategoryCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await context.Categories
            .FindAsync([request.id], cancellationToken);

        Guard.Against.NotFound(request.id.ToString(), category);

        context.Categories.Remove(category);
        await context.SaveChangesAsync(cancellationToken);
    }
}
