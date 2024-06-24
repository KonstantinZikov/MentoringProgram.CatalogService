using Application.Common.Identity;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Domain.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries;

[Authorize($"{Roles.Manager},{Roles.Buyer}")]
public record GetItemsQuery(int? categoryId, int? page) : IRequest<IReadOnlyList<ItemDto>>;

public class GetItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetItemsQuery, IReadOnlyList<ItemDto>>
{
    public async Task<IReadOnlyList<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Item> itemsQuery = context.Items;

        if (request.categoryId != null)
        {
            itemsQuery = itemsQuery.Where(i => i.Category.Id == request.categoryId);
        }

        int page = request.page ?? 1;

        return await itemsQuery
            .Skip((page - 1) * ItemConstants.ItemsPageSize)
            .Take(ItemConstants.ItemsPageSize)     
            .AsNoTracking()
            .ProjectTo<ItemDto>(mapper.ConfigurationProvider)
            .OrderBy(c => c.Id)
            .ToListAsync(cancellationToken);
    }
}
