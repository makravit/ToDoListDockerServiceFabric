using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Web.Api.Models;

namespace ToDoList.Web.Services.Interfaces
{
    public interface ITodoListService
    {
        Task<IEnumerable<TodoItem>> GetTodoItems();

        Task<TodoItem> AddTodoItem(TodoItem todoItem);

        Task<TodoItem> UpdateTodoItem(TodoItem todoItem);

        Task<IEnumerable<TodoItem>> DeleteCompletedTodoItems();
    }
}
