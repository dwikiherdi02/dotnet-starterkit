using System.Net;
using System.Text.Json;
using Apps.Data.Entities;
using Apps.Data.Entities.Rules;
using Apps.Middlewares.Attributes;
using Apps.Services.Interfaces;
using Apps.Utilities._Response;
using Apps.Utilities._ValidationErrorBuilder;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Apps.Controllers
{
    [ApiController]
    [Route("api/todos")]
    [AuthMiddleware]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service) {

            _service = service;
        }

        [HttpGet]
        // [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        // [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetList([FromQuery] TodoEntityQuery queryParams)
        {   
            TodoEntityQueryRule validator = new TodoEntityQueryRule();
            ValidationResult results = validator.Validate(queryParams);

            var authUser = (AuthEntityUserContext?)HttpContext.Items["auth_user"];

            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($"Auth user: {JsonSerializer.Serialize(authUser)}");

            if(!results.IsValid) 
            {
                var errors = _ValidationErrorBuilder.Generate<TodoEntityQuery>(results.Errors);
                
                return new _Response(this)
                            .WithCode(HttpStatusCode.BadRequest)
                            .WithErrors(errors)
                            .Json();
            }

            var list = await _service.FindAll(queryParams);

            return new _Response(this)
                            .WithCode(HttpStatusCode.OK)
                            .WithResult(list)
                            .Json();
        }

        [HttpPost]
        public async Task<ActionResult> PostItem([FromBody] TodoEntityBody body)
        {
            var todo = await _service.Store(body);
            
            if (todo == null)
            {
                // return new _Response(this, HttpStatusCode.BadRequest, "An error occurred while saving your data. Please try again later.").Json();
                return new _Response(this, HttpStatusCode.BadRequest, "Data gagal disimpan, silahkan ulangi kembali.").Json();
            }

            // return new _Response(this, HttpStatusCode.OK, "Data saved successfully")
            return new _Response(this, HttpStatusCode.OK, "Data berhasil disimpan.")
                            .WithResult(todo)
                            .Json();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetItemById(Ulid id)
        {
            var item = await _service.FindById(id);

            if (item == null)
            {
                return new _Response(this, HttpStatusCode.NotFound, "Data tidak ditemukan.").Json();
            }

            return new _Response(this, HttpStatusCode.OK)
                            .WithResult(item)
                            .Json();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutItem(Ulid id, [FromBody] TodoEntityBody body)
        {
            var todo = await _service.Update(id, body);

            if (todo == null)
            {
                return new _Response(this, HttpStatusCode.NotFound, "Data tidak ditemukan.").Json();
            }

            if (todo == false)
            {
                return new _Response(this, HttpStatusCode.BadRequest, "Data gagal disimpan.").Json();
            }

            // return new _Response(this, HttpStatusCode.NoContent, "Data berhasil disimpan.").Json();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Ulid id)
        {
            var todo = await _service.Destroy(id);

            if (todo == null)
            {
                return new _Response(this, HttpStatusCode.NotFound, "Data tidak ditemukan.").Json();
            }

            if (todo == false)
            {
                return new _Response(this, HttpStatusCode.BadRequest, "Data gagal dihapus.").Json();
            }

            // return new _Response(this, HttpStatusCode.NoContent, "Data berhasil dihapus.").Json();
            return NoContent();
        }
    }
}