using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Api.Models;

namespace ToDoList.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ToDoListContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.TodoItems.Any())
            {
                return;   // DB has been seeded
            }

            var todoItems = new List<TodoItem>
            {
                new TodoItem {Name = "Preparar Charla Arquitectura", IsComplete = true},
                new TodoItem {Name = "Pasear al perro"},
                new TodoItem {Name = "Comprar pan"}
            };

            context.TodoItems.AddRange(todoItems);
            context.SaveChanges();
        }
    }
}
