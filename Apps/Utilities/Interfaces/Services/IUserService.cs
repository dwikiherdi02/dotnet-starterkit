using Apps.Data.Entities;

namespace Apps.Utilities.Interfaces.Services
{
    // public interface IUserService : IBaseService<UserEntityQuery, UserEntityBody, UserEntityResponse> {}

    public interface IUserService
    { 
        Task<IEnumerable<UserEntityResponse>> FindAll(UserEntityQuery queryParams);
        Task<UserEntityResponse?> FindById(Guid id);
        Task<UserEntityResponse?> Store(UserEntityBody body);
        Task<bool?> Update(Guid id, UserEntityBodyUpdate body);
        Task<bool?> Destroy(Guid id);
    }
}