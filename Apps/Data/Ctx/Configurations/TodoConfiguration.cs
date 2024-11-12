using Apps.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Apps.Data.Ctx.Configurations
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> entity)
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
        }
    }
}