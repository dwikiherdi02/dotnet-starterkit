using Apps.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apps.Data.Ctx.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            /* var ulidConverter = new ValueConverter<Ulid, string>(
                ulid => ulid.ToString(),
                str => Ulid.Parse(str));
                
            entity
                .Property(p => p.Id)
                .HasConversion(ulidConverter)
                .ValueGeneratedOnAdd(); */

            entity
                .Property(p => p.Id)
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

            // one to one relationship with session
            /* entity
                .HasOne(e => e.Session)
                .WithOne(e => e.User)
                .HasForeignKey<Session>(e => e.UserId); */
            
            // one to many relationship with session
            entity
                .HasMany(e => e.Sessions)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
        }
    }
}