using Apps.Data.Models;

namespace Apps.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> FindUserByEmail(string email);
        Task<User?> FindUserByUsername(string username);
        Task<Session?> StoreSession(Session session);
        Task<Session?> FindSessionById(Ulid id);
        Task<bool> DestroySession(Session item);
    }
}