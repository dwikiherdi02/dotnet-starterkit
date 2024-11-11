

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Apps.Data.Models
{
    [Table("sessions")]
    public class Session
    {
        [Column("id", TypeName = "VARCHAR(26)")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; } = Ulid.NewUlid().ToString();

        [Column("user_id", TypeName = "VARCHAR(26)")]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public required string UserId { get; set; }

        [Column("ip_address", TypeName = "INT UNSIGNED")]
        [Required]
        public int IpAddress { get; set; }

        [Column("user_agent", TypeName = "VARCHAR(255)")]
        [Required]
        public required string UserAgent { get; set; }

        [Column("expired_at", TypeName = "DATETIME(6)")]
        public DateTime? ExpiredAt { get; set; }
        
        [Column("created_at", TypeName = "DATETIME(6)")]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        
        public User User { get; set; } = null!;
    }
}