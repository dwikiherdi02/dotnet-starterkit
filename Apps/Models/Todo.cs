using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apps.Models
{
    [Table("todos")]
    public class Todo
    {
        [Column("id", TypeName = "VARCHAR(36)")]
        [Key]
        public Guid Id { get; set; }

        [Column("name", TypeName = "VARCHAR(100)")]
        [Required]
        public string? Name { get; set; }

        [Column("is_complete")]
        public bool IsComplete { get; set; } = false;

        [Column("created_at", TypeName = "DATETIME")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at", TypeName = "DATETIME")]
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}