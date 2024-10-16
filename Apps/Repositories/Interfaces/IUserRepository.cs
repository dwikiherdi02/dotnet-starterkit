using Apps.Data.Entities;
using Apps.Data.Models;

namespace Apps.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User, UserEntityQuery>
    { }
}