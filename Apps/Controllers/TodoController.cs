using System.Net;
using System.Text.Json;
using Apps.Entities;
using Apps.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Apps.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetList([FromQuery] TodoEntityQuery queryParams)
        {
            var list = await _service.FindAll(queryParams);
            
            return Ok(new {
                code = HttpStatusCode.OK,
                message = HttpStatusCode.OK.ToString(),
                results = list
            });
        }
    }
}