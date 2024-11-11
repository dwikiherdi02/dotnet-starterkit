using Apps.Data.Models;
using Apps.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Apps.Data.Ctx
{
    public class UserContext : BaseContext
    {
        public UserContext(IOptions<DatabaseCfg> database) : base(database, true) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => {
                var ulidConverter = new ValueConverter<Ulid, string>(
                    ulid => ulid.ToString(),
                    str => Ulid.Parse(str));
                    
                entity
                    .Property(p => p.Id)
                    .HasConversion(ulidConverter)
                    .ValueGeneratedOnAdd();

                entity
                    .HasIndex(p => p.Username)
                    .IsUnique();

                entity
                    .HasIndex(p => p.Email)
                    .IsUnique();

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