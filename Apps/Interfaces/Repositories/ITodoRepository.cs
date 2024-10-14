using Apps.Entities;
using Apps.Models;

namespace Apps.Interfaces.Repositories
{
    public interface ITodoRepository
    {
        // Task<IEnumerable<Todo>> FindAll(TodoEntityQuery queryParams);
        Task<(IEnumerable<Todo> list, int count)> FindAll(TodoEntityQuery queryParams);
        Task<Todo?> FindById(Guid id);
        Task<Todo?> Store(Todo todo);
        // Task<bool> Update();
        // Task<bool> Delete();
    }
}