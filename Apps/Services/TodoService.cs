using System.Text.Json;
using Apps.Entities;
using Apps.Interfaces.Repositories;
using Apps.Interfaces.Services;
using Apps.Models;
using Apps.Utilities;

namespace Apps.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepo;

        public TodoService(ITodoRepository todoRepo)
        {
            _todoRepo = todoRepo;
        }

        public async Task<IEnumerable<TodoEntityResponse>> FindAll(TodoEntityQuery queryParams)
        {
            var (lTodo, _) =  await _todoRepo.FindAll(queryParams);
        
            List<TodoEntityResponse> list = new List<TodoEntityResponse>();

            foreach (var todo in lTodo)
            {
                TodoEntityResponse teResponse = _Mapper.MapTo<Todo, TodoEntityResponse>(todo);    
                list.Add(teResponse);
            }

            return list;
        }

        public async Task<TodoEntityResponse?> FindById(Guid id)
        {
            Todo? todo = await _todoRepo.FindById(id);

            if (todo == null)
            {
                return null;
            }

            TodoEntityResponse res = _Mapper.MapTo<Todo, TodoEntityResponse>(todo);

            return res;
        }

        public async Task<TodoEntityResponse?> Store(TodoEntityBody body)
        {
            Todo item = _Mapper.MapTo<TodoEntityBody, Todo>(body);

            Todo? todo = await _todoRepo.Store(item);

            if (todo == null)
            {
                return null;
            }

            TodoEntityResponse res = _Mapper.MapTo<Todo, TodoEntityResponse>(todo); 
            return res;
        }
    }
}
