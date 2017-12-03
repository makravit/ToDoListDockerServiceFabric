using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Api.Data;
using ToDoList.Api.Models;

namespace ToDoList.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/TodoItems")]
    public class TodoItemsController : Controller
    {
        private readonly ToDoListContext _context;

        public TodoItemsController(ToDoListContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodoItems()
        {
            return Ok(await _context.TodoItems.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoItem([FromRoute] int id)
        {
            var todoItem = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return Ok(todoItem);
        }

        [HttpPost]
        public async Task<IActionResult> PostTodoItem([FromBody] TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem([FromRoute] int id, [FromBody] TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            var dbTodoItem = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);

            if (dbTodoItem == null)
            {
                return NotFound();
            }

            dbTodoItem.IsComplete = todoItem.IsComplete;
            dbTodoItem.Name = todoItem.Name;
            await _context.SaveChangesAsync();

            return Ok(dbTodoItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem([FromRoute] int id)
        {
            var todoItem = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("completed")]
        public async Task<IActionResult> DeleteCompletedTodoItems()
        {
            var completedTodoItems = await _context.TodoItems.Where(m => m.IsComplete).ToListAsync();
            _context.TodoItems.RemoveRange(completedTodoItems);
            await _context.SaveChangesAsync();

            return Ok(completedTodoItems);
        }
    }
}