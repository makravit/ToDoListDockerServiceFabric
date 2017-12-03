using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Models;

namespace ToDoList.Api.Data
{
    public class ToDoListContext : DbContext
    {
        public ToDoListContext (DbContextOptions<ToDoListContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
