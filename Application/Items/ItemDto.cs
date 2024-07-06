using Application.Categories.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Items
{
    public class ItemDto
    {
        public int Id { get; init; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; init; }

        public required CategoryDto Category { get; set; }

        public decimal Price { get; set; }

        public required string PriceCurrency { get; set; }

        public required int Amount { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Item, ItemDto>()
                    .ForMember(d => d.ImageUrl, opt => opt.MapFrom(s => s.Image != null ? s.Image.Url : null))
                    .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price.Amount))
                    .ForMember(d => d.PriceCurrency, opt => opt.MapFrom(s => s.Price.Currency.ToString()));
            }
        }
    }
}
