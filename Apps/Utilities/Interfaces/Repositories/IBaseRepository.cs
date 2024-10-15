using Apps.Models;

namespace Apps.Utilities.Interfaces.Repositories
{
    public interface IBaseRepository
    {
        Task<IEnumerable<Todo>> FindAll<TEntity>(TEntity queryParams);
        // Task<Todo> Find();
        // Task<Todo> FindById(string id);
        // Task<bool> Store();
        // Task<bool> Update();
        // Task<bool> Delete();
    }
}