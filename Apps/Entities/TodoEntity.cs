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
        public int? Page { get; set; }

        [FromQuery(Name ="page-size")]
        // [RegularExpression(@"^[0-9]*$", ErrorMessage = "Page size must be number")]
        public int? PageSize { get; set; }
    }
}