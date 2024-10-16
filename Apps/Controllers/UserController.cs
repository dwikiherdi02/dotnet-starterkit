using System.Net;
using Apps.Data.Entities;
using Apps.Utilities.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Apps.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetItems([FromQuery] UserEntityQuery queryParams)
        {
            var list = await _service.FindAll(queryParams);
            
            return Ok(new {
                code = HttpStatusCode.OK,
                message = HttpStatusCode.OK.ToString(),
                results = list
            });
        }

        [HttpPost]
        public async Task<ActionResult> PostItem([FromBody] UserEntityBody body)
        {
            var todo = await _service.Store(body);
            
            if (todo == null)
            {
                return BadRequest(new {
                    code = HttpStatusCode.BadRequest,
                    message = HttpStatusCode.BadRequest.ToString(),
                });
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
                return NotFound(new {
                    code = HttpStatusCode.NotFound,
                    message = HttpStatusCode.NotFound.ToString(),    
                });
            }
            
            return Ok(new {
                code = HttpStatusCode.OK,
                message = HttpStatusCode.OK.ToString(),
                results = item,
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutItem(Guid id, [FromBody] UserEntityBodyUpdate body)
        {
            var todo = await _service.Update(id, body);

            if (todo == null)
            {
                return NotFound(new {
                    code = HttpStatusCode.NotFound,
                    message = HttpStatusCode.NotFound.ToString(),    
                });
            }

            if (todo == false)
            {
                return BadRequest(new {
                    code = HttpStatusCode.BadRequest,
                    message = HttpStatusCode.BadRequest.ToString(),
                });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id)
        {
            var todo = await _service.Destroy(id);

            if (todo == null)
            {
                return NotFound(new {
                    code = HttpStatusCode.NotFound,
                    message = HttpStatusCode.NotFound.ToString(),    
                });
            }

            if (todo == false)
            {
                return BadRequest(new {
                    code = HttpStatusCode.BadRequest,
                    message = HttpStatusCode.BadRequest.ToString(),
                });
            }

            return NoContent();
        }
    }
}