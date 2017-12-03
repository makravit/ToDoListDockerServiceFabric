using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Web.Api.Models;
using ToDoList.Web.Services.Interfaces;

namespace ToDoList.Web.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly HttpClient _httpClient;

        private const string todoItemsRoute = "api/todoItems";

        public TodoListService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://todolist.api:8080/")
            };

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            var response = await _httpClient.GetAsync(todoItemsRoute);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error gettting todo items. Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
            }

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<TodoItem>>(jsonResult);

            return result;
        }

        public async Task<TodoItem> AddTodoItem(TodoItem todoItem)
        {
            var content = new StringContent(JsonConvert.SerializeObject(todoItem), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(todoItemsRoute, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error creating todo item. Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
            }

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TodoItem>(jsonResult);

            return result;
        }

        public async Task<TodoItem> UpdateTodoItem(TodoItem todoItem)
        {
            var content = new StringContent(JsonConvert.SerializeObject(todoItem), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{todoItemsRoute}/{todoItem.Id}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error updating todo item. Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
            }

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TodoItem>(jsonResult);

            return result;
        }

        public async Task<IEnumerable<TodoItem>> DeleteCompletedTodoItems()
        {
            var response = await _httpClient.DeleteAsync($"{todoItemsRoute}/completed");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error deleting completed todo items. Code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
            }

            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<TodoItem>>(jsonResult);

            return result;
        }
    }
}
