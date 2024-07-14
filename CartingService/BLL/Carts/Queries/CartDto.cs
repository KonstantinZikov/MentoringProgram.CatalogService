using AutoMapper;
using DAL.Entities;

namespace BLL.Carts.Queries;

public class CartDto
{
    public CartDto()
    {
        Items = Array.Empty<LineItemDto>();
    }

    public string Id { get; init; }

    public IReadOnlyCollection<LineItemDto> Items { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Cart, CartDto>();  
        }
    }
}
