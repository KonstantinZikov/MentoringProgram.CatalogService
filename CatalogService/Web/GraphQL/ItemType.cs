using Application.Items;
using Application.Items.Commands;
using Application.Items.Queries;
using MediatR;

namespace Web.GraphQL
{
    [ExtendObjectType("Query")]
    public class ItemQuery
    {
        public async Task<IReadOnlyList<ItemDto>> GetItems(int? categoryId, int? page, [Service] ISender sender)
            => await sender.Send(new GetItemsQuery(categoryId, page));
    }

    [ExtendObjectType("Mutation")]
    public class ItemMutation
    {
        public async Task<ItemDto> AddItem(CreateItemCommand command, [Service] ISender sender)
        {
            int id = await sender.Send(command);
            return await sender.Send(new GetItemQuery(id));
        }

        public async Task<ItemDto> UpdateItem(UpdateItemCommand command, [Service] ISender sender)
        {
            await sender.Send(command);
            return await sender.Send(new GetItemQuery(command.Id));
        }

        public async Task<bool> DeleteItem(DeleteItemCommand command, [Service] ISender sender)
        {
            await sender.Send(command);
            return true;
        }
    }

}
