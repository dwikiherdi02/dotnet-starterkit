using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Apps.Interfaces.Models;

namespace Apps.Models
{
    [Table("todos")]
    public class Todo : ISoftDelete
    {
        [Column("id", TypeName = "VARCHAR(36)")]
        [Key]
        public Guid Id { get; set; }

        [Column("name", TypeName = "VARCHAR(100)")]
        [Required]
        public string? Name { get; set; }

        [Column("is_complete")]
        public bool IsComplete { get; set; } = false;

        [Column("created_at", TypeName = "DATETIME(6)")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at", TypeName = "DATETIME(6)")]
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
        
        [Column("deleted_at", TypeName = "DATETIME(6)")]
        public DateTime? DeletedAt { get; set; }
    }
}