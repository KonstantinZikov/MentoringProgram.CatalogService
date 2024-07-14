using Application.Categories.Commands;
using Application.Categories.Queries;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categories")]
    public class CategoryController(
        ISender sender,
        IMapper mapper,
        LinkGenerator linkGenerator)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> GetCategories() => await sender.Send(new GetCategoriesQuery());

        [HttpGet]
        [Route("{id}")]
        public async Task<CategoryDto> GetCategory(int id)
        {
            var category = await sender.Send(new GetCategoryQuery(id));
            return await SetCategoryLinks(category);
        } 

        [HttpPost]
        public async Task<int> AddCategory(CreateCategoryCommand command) => await sender.Send(command);

        [HttpPut]
        public async Task<IResult> UpdateCategory(UpdateCategoryCommand command)
        {
            await sender.Send(command);
            return Results.NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IResult> DeleteCategory(int id)
        {
            await sender.Send(new DeleteCategoryCommand(id));
            return Results.NoContent();
        }

        // I think it's possible to use runtime-generated types like CategoryDtoWithLinks, instead of define them manually.
        // But it requires a more complex implementation.
        private async Task<CategoryDtoWithLinks> SetCategoryLinks(CategoryDto category)
        {
            CategoryDtoWithLinks categoryWithLinks = mapper.Map<CategoryDto, CategoryDtoWithLinks>(category);

            categoryWithLinks.Links =
            [
                new Link
                {
                    Href = linkGenerator.GetPathByAction(HttpContext, nameof(GetCategory)),
                    Rel = "self",
                    Method = "GET"
                },
                new Link
                {
                    Href = linkGenerator.GetPathByAction(HttpContext, nameof(AddCategory)),
                    Rel = "add_category",
                    Method = "POST"
                },
                new Link
                {
                    Href = linkGenerator.GetPathByAction(HttpContext, nameof(UpdateCategory)),
                    Rel = "update_category",
                    Method = "PUT"
                },
                new Link
                {
                    Href = linkGenerator.GetPathByAction(HttpContext, nameof(DeleteCategory), values:new{ id = category.Id }),
                    Rel = "delete_category",
                    Method = "DELETE"
                },
            ];

            return categoryWithLinks;
        }
    }
}
