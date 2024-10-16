using Apps.Data.Entities;

namespace Apps.Services.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoEntityResponse>> FindAll(TodoEntityQuery queryParams);
        Task<TodoEntityResponse?> FindById(Guid id);
        Task<TodoEntityResponse?> Store(TodoEntityBody body);
        Task<bool?> Update(Guid id, TodoEntityBody body);
        Task<bool?> Destroy(Guid id);
    }
}