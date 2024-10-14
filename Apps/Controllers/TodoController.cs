using System.Net;
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


        [HttpPost]
        public async Task<ActionResult> PostItem([FromBody] TodoEntityBody body)
        {
            TodoEntityResponse? todo = await _service.Store(body);
            
            if (todo == null)
            {
                return BadRequest();
            }
            
            return Ok(new {
                code = HttpStatusCode.Created,
                message = HttpStatusCode.Created.ToString(),
                results = todo
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetItemById(Guid id)
        {
            var item = await _service.FindById(id);

            if (item == null)
            {
                return NotFound();
            }
            
            return Ok(new {
                code = HttpStatusCode.OK,
                message = HttpStatusCode.OK.ToString(),
                results = item,
            });
        }
    }
}