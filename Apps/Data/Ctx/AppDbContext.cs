using Apps.Data.Ctx.Interceptors;
using Apps.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Apps.Data.Models;

namespace Apps.Data.Ctx
{
    public class AppDbContext : DbContext
    {

        private readonly IOptions<DatabaseCfg> _database;

        // private readonly bool _withSoftDeleted = false;

        public AppDbContext(IOptions<DatabaseCfg> database) {
            _database = database;
        }

        // public AppDbContext(DbContextOptions<BaseDbContext> options) : base(options) { }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _database.Value.MySql;
            
            optionsBuilder
                    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                    .AddInterceptors(new SoftDeleteInterceptor());
        }
    }
}