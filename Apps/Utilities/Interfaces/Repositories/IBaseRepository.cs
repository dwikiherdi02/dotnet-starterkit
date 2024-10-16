using Apps.Data.Models;

namespace Apps.Utilities.Interfaces.Repositories
{
    public interface IBaseRepository<TModel, TEntityQuery>
    {
        Task<(IEnumerable<TModel> list, int count)> FindAll(TEntityQuery queryParams);
        Task<TModel?> FindById(Guid id);
        Task<TModel?> Store(TModel item);
        Task<bool> Update(TModel item);
        Task<bool> Destroy(TModel item);
    }
}