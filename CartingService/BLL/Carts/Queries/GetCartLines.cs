using Ardalis.GuardClauses;
using AutoMapper;
using BLL.Common.Identity;
using DAL.Common.Interface;
using DAL.Entities;
using MediatR;

namespace BLL.Carts.Queries;

[Authorize($"{Roles.Manager},{Roles.Buyer}")]
public record GetCartLinesQuery(string Id) : IRequest<IReadOnlyCollection<LineItemDto>>;

public class GetCartLinesQueryHandler(IRepository<Cart, string> repository, IMapper mapper)
    : IRequestHandler<GetCartLinesQuery, IReadOnlyCollection<LineItemDto>>
{
    public async Task<IReadOnlyCollection<LineItemDto>> Handle(GetCartLinesQuery request, CancellationToken cancellationToken)
    {
        Cart? cart = await repository.GetById(request.Id);

        Guard.Against.NotFound(request.Id, cart);

        return mapper.Map<List<LineItemDto>>(cart.Items);
    }
}
