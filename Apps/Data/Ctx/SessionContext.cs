
using Apps.Config;
using Apps.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Options;

namespace Apps.Data.Ctx
{
    public class SessionContext : BaseContext
    {
        public SessionContext(IOptions<DatabaseCfg> database) : base(database, true) {}

        public DbSet<Session> Session { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Session>(entity => {
                
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
                    .WithOne(e => e.Session)
                    .HasForeignKey<Session>(e => e.UserId)
                    .IsRequired();
                
            });
        }
    }
}