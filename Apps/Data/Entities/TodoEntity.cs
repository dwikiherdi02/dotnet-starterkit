using System.ComponentModel;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Apps.Data.Entities
{
    public class TodoEntityQuery
    {   
        [FromQuery(Name ="search")]
        // [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Search must be email format")]
        public string? Search { get; set; }

        [FromQuery(Name = "page")]
        [DefaultValue(1)]
        // [RegularExpression(@"^\d{1,}$", ErrorMessage = "Page must be number and minimum length is 1")]
        public int Page { get; set; }

        [FromQuery(Name = "pagesize")]
        [DefaultValue(10)]
        // [RegularExpression(@"^[0-9]*$", ErrorMessage = "Page size must be number")]
        public int PageSize { get; set; }

        // [FromQuery(Name = "orders")]
        // public string? Orders { get; set; }
    }

    public class TodoEntityBody
    {
        [JsonPropertyName("name")]
        [DefaultValue("Todo 1st")]
        public string? Name { get; set; }

        [JsonPropertyName("is_complete")]
        [DefaultValue(false)]
        public bool IsComplete { get; set; }
    }

    public class TodoEntityResponse
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("is_complete")]
        public bool IsComplete { get; set; }

        // [JsonPropertyName("created_at")]
        // public string? CreatedAt { get; set; }
    }
}