using BLL.Carts.Commands;
using BLL.Carts.Queries;
using MediatR;

namespace BLL.Carts.Services
{
    public class CartService : ICartService
    {
        public CartService(ISender sender)
        {
            _sender = sender;
        }

        private readonly ISender _sender;

        public async Task<int> AddLineToCartWithCartCreation(AddItemToCartCommand command)
        {
            CartDto? cart = await _sender.Send(new GetCartQuery(command.CartId));

            if (cart == null)
            {
                await _sender.Send(new CreateCartCommand(command.CartId));
            }

            return await _sender.Send(command);
        }
    }
}
