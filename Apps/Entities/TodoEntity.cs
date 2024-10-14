using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Apps.Entities
{
    public class TodoEntityQuery
    {   
        [FromQuery(Name ="search")]
        // [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Search must be email format")]
        public string? Search { get; set; }

        [FromQuery(Name = "page")]
        // [RegularExpression(@"^\d{1,}$", ErrorMessage = "Page must be number and minimum length is 1")]
        public int Page { get; set; }

        [FromQuery(Name ="page-size")]
        // [RegularExpression(@"^[0-9]*$", ErrorMessage = "Page size must be number")]
        public int PageSize { get; set; }
    }

    public class TodoEntityBody
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("is_complete")]
        public bool IsComplete { get; set; }
    }

    public class TodoEntityResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("is_complete")]
        public bool IsComplete { get; set; }

        // [JsonPropertyName("created_at")]
        // public string? CreatedAt { get; set; }
    }
}