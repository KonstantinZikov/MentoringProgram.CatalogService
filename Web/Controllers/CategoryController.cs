using Application.Carts.Commands;
using Application.Carts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    
    public class CategoryController : ControllerBase
    {
        private readonly ISender _sender;

        public CategoryController(ILogger<CategoryController> logger, ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [Route("api/categories")]
        public async Task<IEnumerable<CategoryDto>> GetCategories() => await _sender.Send(new GetCategoriesQuery());

        [HttpPost]
        [Route("api/categories")]
        public async Task<int> AddCategory(CreateCategoryCommand command) => await _sender.Send(command);

        [HttpPut]
        [Route("api/categories")]
        public async Task AddCategory(UpdateCategoryCommand command) => await _sender.Send(command);

        [HttpDelete]
        [Route("api/categories")]
        public async Task AddCategory(DeleteCategoryCommand command) => await _sender.Send(command);

        [HttpGet]
        [Route("api/categories/{id}")]
        public async Task<CategoryDto> GetCategory(int id) => await _sender.Send(new GetCategoryQuery(id));
    }
}
