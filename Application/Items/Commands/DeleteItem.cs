using Application.Common.Interface;
using Domain.Entities;
using MediatR;

namespace Application.Items.Commands;

public record DeleteItemCommand(int id) : IRequest;

public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        Item? item = await _context.Items
            .FindAsync([request.id], cancellationToken);

        if (item != null)
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
