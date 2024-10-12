using Apps.Models;

namespace Apps.Interfaces.Repositories
{
    public interface IBaseRepository
    {
        Task<IEnumerable<Todo>> FindAll();
        Task<Todo> Find();
        Task<Todo> FindById(string id);
        Task<bool> Store();
        Task<bool> Update();
        Task<bool> Delete();
    }
}