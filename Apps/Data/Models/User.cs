using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Apps.Data.Models.Interfaces;

namespace Apps.Data.Models
{
    [Table("users")]
    public class User : ISoftDelete
    {
        [Column("id", TypeName = "VARCHAR(26)")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        // public Ulid Id { get; set; } = Ulid.NewUlid();
        public string Id { get; set; } = Ulid.NewUlid().ToString();

        [Column("name", TypeName = "VARCHAR(100)")]
        public string Name { get; set; } = string.Empty;
        
        [Column("username", TypeName = "VARCHAR(100)")]
        [Required]
        public required string Username { get; set; }
        
        [Column("email", TypeName = "VARCHAR(100)")]
        [Required]
        public required string Email { get; set; }
        
        [Column("password", TypeName = "VARCHAR(60)")]
        [Required]
        public required string Password { get; set; }

        [Column("created_at", TypeName = "DATETIME(6)")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at", TypeName = "DATETIME(6)")]
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("deleted_at", TypeName = "DATETIME(6)")]
        public DateTime? DeletedAt { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
        
        // public Session? Session { get; set; }

        // public ICollection<Session> Sessions { get; } = new List<Session>();
    }
}