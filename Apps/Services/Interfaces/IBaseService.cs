namespace Apps.Services.Interfaces
{
    public interface IBaseService<TEntityQuery, TEntityBody, TEntityResponse>
    {
        Task<IEnumerable<TEntityResponse>> FindAll(TEntityQuery queryParams);
        Task<TEntityResponse?> FindById(Guid id);
        Task<TEntityResponse?> Store(TEntityBody body);
        Task<bool?> Update(Guid id, TEntityBody body);
        Task<bool?> Destroy(Guid id);
    }
}