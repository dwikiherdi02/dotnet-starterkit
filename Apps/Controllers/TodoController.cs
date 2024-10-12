using System.Net;
using System.Text.Json;
using Apps.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Apps.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodoController : ControllerBase
    {
        public TodoController() {}

        [HttpGet]
        public ActionResult GetList([FromQuery] TodoEntityQuery queryParams)
        {
            Console.WriteLine(JsonSerializer.Serialize(queryParams));
            
            return Ok(new {
                Code = HttpStatusCode.OK,
                Message = HttpStatusCode.OK.ToString(),
            });
        }
    }
}