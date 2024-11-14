using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Apps.Data.Entities
{
    public class AuthEntityLoginBody
    {
        [JsonPropertyName("email")]
        [DefaultValue("dwiki@beit.co.id")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        [DefaultValue("password")]
        public string Password { get; set; } = string.Empty;
    }

    public class AuthEntityLoginResponse
    {
        [JsonPropertyName("user")]
        public AuthEntityUserProp? User { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; } = string.Empty;
    }

    public class AuthEntityUserProp
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
    }
}