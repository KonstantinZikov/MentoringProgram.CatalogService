using Application.Carts.Queries;
using AutoMapper;

namespace Web.Models
{
    public class CategoryDtoWithLinks : CategoryDto
    {
        public IReadOnlyCollection<Link>? Links { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<CategoryDto, CategoryDtoWithLinks>()
                    .ForMember(c => c.Links, opt => opt.Ignore());
            }
        }
    }
}
