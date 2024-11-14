using System.Net;
using Apps.Data.Entities;
using Apps.Middlewares.Attributes;
using Apps.Services.Interfaces;
using Apps.Utilities._Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace Apps.Controllers
{
    [ApiController]
    [Route("api/users")]
    [AuthMiddleware]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers([FromQuery] UserEntityQuery queryParams)
        {
            var list = await _service.FindAll(queryParams);
            
            return new _Response(this, HttpStatusCode.OK)
                            .WithResult(list)
                            .Json();
        }

        [HttpPost]
        public async Task<ActionResult> PostUser([FromBody] UserEntityBody body)
        {
            var user = await _service.Store(body);
            
            if (user == null)
            {
                return new _Response(this, HttpStatusCode.BadRequest, "Data gagal disimpan, silahkan ulangi kembali.").Json();
            }

            return new _Response(this, HttpStatusCode.OK, "Data berhasil disimpan.")
                            .WithResult(user)
                            .Json();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUserById(Ulid id)
        {
            var user = await _service.FindById(id);

            if (user == null)
            {
                return new _Response(this, HttpStatusCode.NotFound, "Data tidak ditemukan.").Json();
            }

            return new _Response(this, HttpStatusCode.OK)
                            .WithResult(user)
                            .Json();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(Ulid id, [FromBody] UserEntityBodyUpdate body)
        {
            var user = await _service.Update(id, body);

            if (user == null)
            {
                return new _Response(this, HttpStatusCode.NotFound, "Data tidak ditemukan.").Json();
            }

            if (user == false)
            {
                return new _Response(this, HttpStatusCode.BadRequest, "Data gagal disimpan.").Json();
            }

            // return new _Response(this, HttpStatusCode.NoContent).Json();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Ulid id)
        {
            var user = await _service.Destroy(id);

            if (user == null)
            {
                return new _Response(this, HttpStatusCode.NotFound, "Data tidak ditemukan.").Json();
            }

            if (user == false)
            {
                return new _Response(this, HttpStatusCode.BadRequest, "Data gagal dihapus.").Json();
            }

            // return new _Response(this, HttpStatusCode.NoContent).Json();
            return NoContent();
        }
    }
}