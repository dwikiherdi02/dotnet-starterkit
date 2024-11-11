using System.ComponentModel;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Apps.Data.Entities
{
    public class UserEntity
    {
        public UserEntityQuery? Query { get; set; }

        public UserEntityBody? PostBody { get; set; }

        public UserEntityResponse? Response { get; set; }
    }

    public class UserEntityQuery
    {   
        [FromQuery(Name ="search")]
        public string? Search { get; set; }

        [FromQuery(Name = "page")]
        [DefaultValue(1)]
        public int Page { get; set; }

        [FromQuery(Name = "pagesize")]
        [DefaultValue(10)]
        public int PageSize { get; set; }

        // [FromQuery(Name = "orders")]
        // public string? Orders { get; set; }
    }

    public class UserEntityBody
    {
        [JsonPropertyName("name")]
        [DefaultValue("Dwiki Herdiansyah")]
        public string? Name { get; set; }

        [JsonPropertyName("username")]
        [DefaultValue("dwikiherdi02")]
        public string? Username { get; set; }

        [JsonPropertyName("email")]
        [DefaultValue("dwiki@beit.co.id")]
        public string? Email { get; set; }
        
        [JsonPropertyName("password")]
        [DefaultValue("password")]
        public string? Password { get; set; }
    }

    public class UserEntityBodyUpdate
    {
        [JsonPropertyName("name")]
        [DefaultValue("Dwiki Herdiansyah")]
        public string? Name { get; set; }

        [JsonPropertyName("username")]
        [DefaultValue("dwikiherdi02")]
        public string? Username { get; set; }

        [JsonPropertyName("email")]
        [DefaultValue("dwiki@beit.co.id")]
        public string? Email { get; set; }
    }

    public class UserEntityResponse
    {
        [JsonPropertyName("id")]
        public Ulid Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; set; }
    }
}