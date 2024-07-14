using BLL.Carts.Commands;

namespace BLL.Carts.Services
{
    public interface ICartService
    {
        Task<int> AddLineToCartWithCartCreation(AddItemToCartCommand command);
    }
}
