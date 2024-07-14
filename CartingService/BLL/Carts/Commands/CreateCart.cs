using BLL.Common.Identity;
using DAL.Common.Interface;
using DAL.Entities;
using MediatR;

namespace BLL.Carts.Commands;

[Authorize($"{Roles.Manager},{Roles.Buyer}")]
public record CreateCartCommand(string id) : IRequest;

public class CreateCartCommandHandler(IRepository<Cart, string> repository) : IRequestHandler<CreateCartCommand>
{
    public async Task Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var cart = new Cart { Id = request.id };
        await repository.Insert(cart);
    }
}
