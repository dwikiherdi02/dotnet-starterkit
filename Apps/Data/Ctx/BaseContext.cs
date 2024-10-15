using Apps.Settings;
using Apps.Utilities.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Apps.Data.Ctx
{
    public class BaseContext : DbContext
    {

        private readonly IOptions<Database> _database;

        private readonly bool? _withSoftDeleted = false;

        public BaseContext(IOptions<Database> database, bool withSoftDeleted = false) {
            _database = database;
            _withSoftDeleted = withSoftDeleted;
        }

        // public BaseDbContext(DbContextOptions<BaseDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _database.Value.MySql;
            if (_withSoftDeleted== false)
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