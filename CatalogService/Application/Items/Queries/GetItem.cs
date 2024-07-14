using Application.Common.Identity;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries;

[Authorize($"{Roles.Manager},{Roles.Buyer}")]
public record GetItemQuery(int id) : IRequest<ItemDto>;

public class GetItemQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetItemQuery, ItemDto>
{
    public async Task<ItemDto> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        Item? item = await context.Items
            .Include(i => i.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == request.id, cancellationToken: cancellationToken);

        return mapper.Map<ItemDto>(item);
    }
}
