using AutoMapper;
using DAL.ValueObjects;

namespace BLL.Carts.Queries
{
    public class LineItemDto
    {
        public required int Id { get; init; }

        public required int ProductId { get; init; }

        public required string Name { get; init; }

        public string? ImageUrl { get; init; }

        public string? ImageAlt { get; init; }

        public decimal Price { get; set; }

        public required string PriceCurrency { get; set; }

        public required int Quantity { get; init; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<LineItem, LineItemDto>()
                    .ForMember(d => d.ImageUrl, opt => opt.MapFrom(s => s.Image != null ? s.Image.Url : null))
                    .ForMember(d => d.ImageAlt, opt => opt.MapFrom(s => s.Image != null ? s.Image.Alt : null))
                    .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price.Amount))
                    .ForMember(d => d.PriceCurrency, opt => opt.MapFrom(s => s.Price.Currency.ToString()));
            }
        }
    }
}
