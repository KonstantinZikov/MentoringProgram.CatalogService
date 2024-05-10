using Application.Common.Interface;
using Domain.Entities;
using MediatR;

namespace Application.Carts.Commands;

public record DeleteCategoryCommand(int id) : IRequest;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await _context.Categories
            .FindAsync([request.id], cancellationToken);

        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
        }      
    }
}
