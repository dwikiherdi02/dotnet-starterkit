using Apps.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apps.Data.Ctx.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> entity)
        {
            // Mengonversi ULID ke string
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

            // one to one relationship with user
            entity
                .HasOne(e => e.User)
                .WithMany(e => e.Sessions)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
        }
    }
}