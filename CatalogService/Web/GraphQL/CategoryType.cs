using Application.Categories.Commands;
using Application.Categories.Queries;
using MediatR;

namespace Web.GraphQL
{
    [ExtendObjectType("Query")]
    public class CategoryQuery
    {
        public async Task<IReadOnlyList<CategoryDto>> GetCategories([Service] ISender sender) 
            => await sender.Send(new GetCategoriesQuery());

        public async Task<CategoryDto?> GetCategory(int id, [Service] ISender sender) 
            => await sender.Send(new GetCategoryQuery(id));
    }

    [ExtendObjectType("Mutation")]
    public class CategoryMutation
    {
        public async Task<CategoryDto> AddCategory(CreateCategoryCommand command, [Service] ISender sender)
        {
            int id = await sender.Send(command);
            return await sender.Send(new GetCategoryQuery(id));
        }

        public async Task<CategoryDto> UpdateCategory(UpdateCategoryCommand command, [Service] ISender sender)
        {
            await sender.Send(command);
            return await sender.Send(new GetCategoryQuery(command.Id));
        }

        public async Task<bool> DeleteCategory(int id, [Service] ISender sender)
        {
            await sender.Send(new DeleteCategoryCommand(id));
            return true;
        }
    }
}
