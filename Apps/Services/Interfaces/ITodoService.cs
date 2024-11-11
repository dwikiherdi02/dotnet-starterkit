using Apps.Data.Entities;

namespace Apps.Services.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoEntityResponse>> FindAll(TodoEntityQuery queryParams);
        Task<TodoEntityResponse?> FindById(Ulid id);
        Task<TodoEntityResponse?> Store(TodoEntityBody body);
        Task<bool?> Update(Ulid id, TodoEntityBody body);
        Task<bool?> Destroy(Ulid id);
    }
}