using Apps.Data.Entities;
using Apps.Data.Models;

namespace Apps.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> Login(AuthEntityLoginBody body);
        Task<AuthEntityLoginResponse> GenerateToken(User user);
        Task<AuthEntityRefreshTokenResponse> RefreshToken(string rToken);
    }
}