using Application.Common.Interface;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Items.Queries;

public record GetItemsQuery(int? categoryId, int? page) : IRequest<IReadOnlyList<ItemDto>>;

public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, IReadOnlyList<ItemDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Item> itemsQuery = _context.Items;

        if (request.categoryId != null)
        {
            itemsQuery = itemsQuery.Where(i => i.Category.Id == request.categoryId);
        }

        int page = request.page ?? 1;

        return await itemsQuery
            .Skip((page - 1) * ItemConstants.ItemsPageSize)
            .Take(ItemConstants.ItemsPageSize)     
            .AsNoTracking()
            .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
            .OrderBy(c => c.Id)
            .ToListAsync(cancellationToken);
    }
}
