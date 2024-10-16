using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Apps.Utilities.Interfaces.Models;

namespace Apps.Data.Models
{
    [Table("users")]
    public class User : ISoftDelete
    {
        [Column("id", TypeName = "VARCHAR(36)")]
        public Guid Id { get; set; }

        [Column("name", TypeName = "VARCHAR(100)")]
        [Required]
        public string? Name { get; set; }
        
        [Column("username", TypeName = "VARCHAR(100)")]
        [Required]
        public string? Username { get; set; }
        
        [Column("email", TypeName = "VARCHAR(100)")]
        [Required]
        public string? Email { get; set; }
        
        [Column("password", TypeName = "VARCHAR(60)")]
        [Required]
        public string? Password { get; set; }

        [Column("created_at", TypeName = "DATETIME(6)")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at", TypeName = "DATETIME(6)")]
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("deleted_at", TypeName = "DATETIME(6)")]
        public DateTime? DeletedAt { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}