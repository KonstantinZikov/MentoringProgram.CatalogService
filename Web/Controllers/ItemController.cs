using Application.Items;
using Application.Items.Commands;
using Application.Items.Queries;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/items")]
    public class ItemController : ControllerBase
    {
        private readonly ISender _sender;

        public ItemController(ILogger<CategoryController> logger, ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItems(int? categoryId, [FromQuery(Name = "p")] int? page) 
            =>  await _sender.Send(new GetItemsQuery(categoryId, page));

        [HttpPost]
        public async Task<int> AddItem(CreateItemCommand command) => await _sender.Send(command);

        [HttpPut]
        public async Task<IResult> UpdateItem(UpdateItemCommand command)
        {
            await _sender.Send(command);
            return Results.NoContent();
        }

        [HttpDelete]
        public async Task<IResult> DeleteItem(DeleteItemCommand command)
        {
            await _sender.Send(command);
            return Results.NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ItemDto> GetItem(int id) => await _sender.Send(new GetItemQuery(id));
    }
}
