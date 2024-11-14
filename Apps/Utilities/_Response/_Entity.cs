using System.Net;
using System.Text.Json.Serialization;

namespace Apps.Utilities._Response
{
    public class _Entity
    {
        [JsonPropertyName("code")]
        public HttpStatusCode Code { get; set; }        
        
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("results")]
        public object? Results { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("error")]
        public string? Error { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("errors")]
        public object? Errors { get; set; }
    }
}