using Ardalis.GuardClauses;
using BLL.Common.Identity;
using DAL.Common.Interface;
using DAL.Entities;
using DAL.ValueObjects;
using MediatR;

namespace BLL.Carts.Commands;

[Authorize($"{Roles.Manager},{Roles.Buyer}")]
public record RemoveItemFromCartCommand : IRequest
{ 
    public required string CartId { get; set; }

    public required int LineItemId { get; set; }
}


public class RemoveItemFromCartCommandHandler(IRepository<Cart, string> repository)
    : IRequestHandler<RemoveItemFromCartCommand>
{
    public async Task Handle(RemoveItemFromCartCommand request, CancellationToken cancellationToken)
    {
        Cart? cart = await repository.GetById(request.CartId);

        Guard.Against.NotFound(request.CartId, cart);

        LineItem? itemToRemove = cart.Items.FirstOrDefault(i => i.Id == request.LineItemId);

        Guard.Against.NotFound(request.LineItemId, itemToRemove);

        cart.Items.Remove(itemToRemove);
        await repository.Update(cart);
    }
}
