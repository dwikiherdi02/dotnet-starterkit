using Apps.Data.Entities;
using Apps.Data.Models;

namespace Apps.Utilities.Interfaces.Repositories
{
    public interface ITodoRepository
    {
        Task<(IEnumerable<Todo> list, int count)> FindAll(TodoEntityQuery queryParams);
        Task<Todo?> FindById(Guid id);
        Task<Todo?> Store(Todo item);
        Task<bool> Update(Todo item);
        Task<bool> Destroy(Todo item);
    }
}