using Apps.Data.Ctx.Interceptors;
using Apps.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Storage;

namespace Apps.Data.Ctx
{
    public class BaseContext : DbContext
    {

        private readonly IOptions<DatabaseCfg> _database;

        private readonly bool _withSoftDeleted = false;

        public BaseContext(IOptions<DatabaseCfg> database, bool withSoftDeleted = false) {
            _database = database;
            _withSoftDeleted = withSoftDeleted;
        }

        // public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _database.Value.MySql;
            if (_withSoftDeleted == false)
            {
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
            else {
                optionsBuilder
                    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                    .AddInterceptors(new SoftDeleteInterceptor());
            }
        }
    }
}