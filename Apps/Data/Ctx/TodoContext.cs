using Apps.Data.Models;
using Apps.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Apps.Utilities._Ulid;

namespace Apps.Data.Ctx
{
    public class TodoContext : BaseContext
    {
        public TodoContext(IOptions<DatabaseCfg> database) : base(database, true) {}

        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>(entity => {
                entity.HasKey(p => p.Id);

                // Mengonversi ULID ke string
                /* var ulidConverter = new ValueConverter<Ulid, string>(
                    ulid => ulid.ToFormattedString(),
                    str => _Ulid.FromFormattedString(str)); */
                var ulidConverter = new ValueConverter<Ulid, string>(
                    ulid => ulid.ToString(),
                    str => Ulid.Parse(str));
                
                entity
                    .Property(p => p.Id)
                    .HasConversion(ulidConverter)
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