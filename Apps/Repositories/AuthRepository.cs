using Apps.Config;
using Apps.Data.Ctx;
using Apps.Data.Models;
using Apps.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Apps.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _dbCtx;

        public AuthRepository(AppDbContext dbCtx)
        {
            _dbCtx = dbCtx;
        }

        public async  Task<User?> FindUserByEmail(string email)
        {
            return await _dbCtx.Users.FirstOrDefaultAsync(q => q.Email == email);
        }

        public async  Task<User?> FindUserByUsername(string username)
        {
            return await _dbCtx.Users.FirstOrDefaultAsync(q => q.Username == username);
        }

        public async Task<Session?> StoreSession(Session session)
        {
            using var transaction = _dbCtx.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("StoreSession");

                _dbCtx.Sessions.Add(session);
                await _dbCtx.SaveChangesAsync();
                
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackToSavepointAsync("StoreSession");

                return null;
            }

            return session;
        }

    }
}