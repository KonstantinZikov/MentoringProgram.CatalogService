using AutoMapper;
using BLL.Common.Identity;
using DAL.Common.Interface;
using DAL.Entities;
using MediatR;

namespace BLL.Carts.Queries;

[Authorize($"{Roles.Manager},{Roles.Buyer}")]
public record GetCartQuery(string Id) : IRequest<CartDto?>;

public class GetCartQueryHandler(IRepository<Cart, string> repository, IMapper mapper)
    : IRequestHandler<GetCartQuery, CartDto?>
{
    public async Task<CartDto?> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        Cart? cart = await repository.GetById(request.Id);
        return mapper.Map<CartDto>(cart);
    }
}
