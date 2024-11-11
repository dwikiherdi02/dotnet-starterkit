using Apps.Data.Entities;

namespace Apps.Services.Interfaces
{
    // public interface IUserService : IBaseService<UserEntityQuery, UserEntityBody, UserEntityResponse> {}

    public interface IUserService
    { 
        Task<IEnumerable<UserEntityResponse>> FindAll(UserEntityQuery queryParams);
        Task<UserEntityResponse?> FindById(Ulid id);
        Task<UserEntityResponse?> Store(UserEntityBody body);
        Task<bool?> Update(Ulid id, UserEntityBodyUpdate body);
        Task<bool?> Destroy(Ulid id);
    }
}