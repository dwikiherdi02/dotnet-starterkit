using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Apps.Data.Entities
{
    public class AuthEntityLoginBody
    {
        [JsonPropertyName("email")]
        // [Required]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        // [Required]
        public string Password { get; set; } = string.Empty;
    }
}