using Apps.Data.Models;
using Apps.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Apps.Data.Ctx
{
    public class TodoContext : BaseContext
    {
        public TodoContext(IOptions<Database> database) : base(database, true) {}

        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>(entity => {
                entity
                    .Property(p => p.Id)
                    .ValueGeneratedOnAdd();

                entity
                    .Property(p => p.CreatedAt)
                    .ValueGeneratedOnAdd();

                entity
                    .Property(p => p.UpdatedAt)
                    .ValueGeneratedNever();

                entity
                    .Property(p => p.DeletedAt)
                    .ValueGeneratedNever();

                // Automatically Filtering Soft-Deleted Data
                entity.HasQueryFilter(p => !p.IsDeleted);

                // Faster Queries Using Filtered Index
                entity
                    .HasIndex(p => p.IsDeleted)
                    .HasFilter("is_deleted = 0");
            });
        }
    }
}