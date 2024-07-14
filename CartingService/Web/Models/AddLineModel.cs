using AutoMapper;
using BLL.Carts.Commands;
using System.Text.Json.Serialization;

namespace Web.Models
{
    public class AddLineModel
    {
        [JsonIgnore]
        public string? CartId { get; set; }

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
                CreateMap<AddLineModel, AddItemToCartCommand>();
            }
        }

    }
}
