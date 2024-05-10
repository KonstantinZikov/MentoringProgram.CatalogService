using Application.Items.Commands;
using Application.Items.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    
    public class ItemController : ControllerBase
    {
        private readonly ISender _sender;

        public ItemController(ILogger<CategoryController> logger, ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [Route("api/items")]
        public async Task<IEnumerable<ItemDto>> GetItems() => await _sender.Send(new GetItemsQuery());

        [HttpPost]
        [Route("api/items")]
        public async Task<int> AddItem(CreateItemCommand command) => await _sender.Send(command);

        [HttpPut]
        [Route("api/items")]
        public async Task AddItem(UpdateItemCommand command) => await _sender.Send(command);

        [HttpDelete]
        [Route("api/items")]
        public async Task AddItem(DeleteItemCommand command) => await _sender.Send(command);

        [HttpGet]
        [Route("api/items/{id}")]
        public async Task<ItemDto> GetItem(int id) => await _sender.Send(new GetItemQuery(id));
    }
}
