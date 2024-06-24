using Application.Common.Identity;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Identity;
using MediatR;

namespace Application.Items.Commands;

[Authorize(Roles.Manager)]
public record DeleteItemCommand(int id) : IRequest;

public class DeleteItemCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteItemCommand>
{
    public async Task Handle(DeleteItemCommand request, CancellationToken cancellationToken)
    {
        Item? item = await context.Items
            .FindAsync([request.id], cancellationToken);

        if (item != null)
        {
            context.Items.Remove(item);
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
