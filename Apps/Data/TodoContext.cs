using Apps.Models;
using Apps.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Apps.Data
{
    public class TodoContext : BaseContext
    {
        public TodoContext(IOptions<Database> database) : base(database) {}

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
            });
        }
    }
}