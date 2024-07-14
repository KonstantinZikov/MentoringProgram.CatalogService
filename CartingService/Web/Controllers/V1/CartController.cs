using Ardalis.GuardClauses;
using Asp.Versioning;
using AutoMapper;
using BLL.Carts.Commands;
using BLL.Carts.Queries;
using BLL.Carts.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CartController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;

        public CartController(ISender sender, IMapper mapper, ICartService cartService)
        {
            _sender = sender;
            _mapper = mapper;
            _cartService = cartService;
        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<CartDto> GetCart(string id)
        {
            CartDto? cart = await _sender.Send(new GetCartQuery(id));
            Guard.Against.NotFound(id, cart);
            return cart;
        }

        [HttpPost]
        [Route("{cartId}/lines")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<int> AddLine([FromRoute]string cartId, [FromBody]AddLineModel model) 
        {
            model.CartId = cartId;
            return await _cartService.AddLineToCartWithCartCreation(_mapper.Map<AddItemToCartCommand>(model));
        }

        [HttpDelete]
        [Route("{cartId}/lines/{lineId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IResult> DeleteLine(string cartId, int lineId)
        {
            await _sender.Send(new RemoveItemFromCartCommand { CartId = cartId, LineItemId = lineId });
            return Results.NoContent();
        }
    }
}
