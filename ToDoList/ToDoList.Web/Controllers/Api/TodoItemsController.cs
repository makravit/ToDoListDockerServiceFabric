using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoList.Web.Api.Models;
using ToDoList.Web.Services.Interfaces;

namespace ToDoList.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/TodoItems")]
    public class TodoItemsController : Controller
    {
        private readonly ITodoListService _todoListService;

        public TodoItemsController(ITodoListService todoListService)
        {
            _todoListService = todoListService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            return Ok(await _todoListService.GetTodoItems());
        }

        [HttpPost]
        public async Task<IActionResult> PostTodoItem([FromBody] TodoItem todoItem)
        {
            return Ok(await _todoListService.AddTodoItem(todoItem));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem([FromRoute] int id, [FromBody] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            return Ok(await _todoListService.UpdateTodoItem(todoItem));
        }

        [HttpDelete("completed")]
        public async Task<IActionResult> DeleteCompletedTodoItems()
        {
            return Ok(await _todoListService.DeleteCompletedTodoItems());
        }
    }
}