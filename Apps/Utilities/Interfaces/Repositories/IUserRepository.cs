using Apps.Data.Entities;
using Apps.Data.Models;

namespace Apps.Utilities.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User, UserEntityQuery>
    { }
}