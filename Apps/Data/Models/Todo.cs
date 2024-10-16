using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Apps.Data.Models.Interfaces;

namespace Apps.Data.Models
{
    [Table("todos")]
    public class Todo : ISoftDelete
    {
        [Column("id", TypeName = "VARCHAR(36)")]
        [Key]
        public Guid Id { get; set; }

        [Column("name", TypeName = "VARCHAR(100)")]
        [Required]
        public required string Name { get; set; }

        [Column("is_complete")]
        public bool IsComplete { get; set; } = false;

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