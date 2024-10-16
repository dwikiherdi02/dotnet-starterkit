using Apps.Data.Ctx;
using Apps.Data.Models;
using Apps.Data.Entities;
using Apps.Utilities.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Apps.Repositories.ExtensionMethods;

namespace Apps.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _userCtx;

        public UserRepository(UserContext userCtx)
        {
            _userCtx = userCtx;
        }

        public async Task<(IEnumerable<User> list, int count)> FindAll(UserEntityQuery queryParams)
        {
            var query = _userCtx.Users.AsQueryable();

            if (queryParams.Search != null)
            {
                query = query.Where(p =>
                    (
                        p.Name.Contains(queryParams.Search)
                        || p.Username.Contains(queryParams.Search)
                        || p.Email.Contains(queryParams.Search)
                    )
                );
            }

            // * Generate count before limiting
            var count = await query.CountAsync();

            if (queryParams.PageSize > 0)
            {
                if (queryParams.Page > 0)
                {
                    query = query.Skip((queryParams.Page - 1) * queryParams.PageSize);
                }

                query = query.Take(queryParams.PageSize);
            }

            query = query.OrderByColumn("CreatedAt", "desc");

            // * Generate list of todo
            var list = await query.ToListAsync();

            return (list, count);
        }

        public async Task<User?> FindById(Guid id)
        {
            return await _userCtx.Users.FindAsync(id);
        }

        public async Task<User?> Store(User item)
        {
            using var transaction = _userCtx.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("StoreUserItem");

                _userCtx.Users.Add(item);
                await _userCtx.SaveChangesAsync();
                
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("StoreUserItem");

                return null;
            }

            return item;
        }

        public async Task<bool> Update(User item)
        {
            using var transaction = _userCtx.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("UpdateUserItem");

                item.UpdatedAt = DateTime.UtcNow;

                _userCtx.Entry(item).State = EntityState.Modified;

                await _userCtx.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("UpdateUserItem");

                return false;
            }
            return true;
        }

        public async Task<bool> Destroy(User item)
        {
            using var transaction = _userCtx.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("DestroyTodoItem");

                _userCtx.Users.Remove(item);
                await _userCtx.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("DestroyTodoItem");
                return false;
            }

            return true;
        }

    }
}