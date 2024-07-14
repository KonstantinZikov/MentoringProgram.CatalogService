using AutoMapper;
using Domain.Entities;

namespace Application.Categories.Queries;

public class CategoryDto
{
    public int Id { get; init; }

    public required string Name { get; set; }

    public string? ImageUrl { get; set; }

    public int? ParentCategoryId { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(d => d.ImageUrl, opt => opt.MapFrom(s => s.Image != null ? s.Image.Url : null))
                .ForMember(d => d.ParentCategoryId, opt => opt.MapFrom(s => s.ParentCategory != null ? s.ParentCategory.Id : (int?)null));
        }
    }
}
