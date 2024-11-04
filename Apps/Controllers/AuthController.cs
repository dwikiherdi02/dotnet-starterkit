using System.Net;
using System.Text.Json;
using Apps.Data.Entities;
using Apps.Data.Entities.Rules;
using Apps.Utilities._Response;
using Apps.Utilities._ValidationErrorBuilder;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Apps.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        
        [HttpPost("login")]
        // public async Task<ActionResult> PostLogin()
        public ActionResult PostLogin([FromBody] AuthEntityLoginBody body)
        {   
            AuthEntityLoginBodyRule validator = new AuthEntityLoginBodyRule();
            ValidationResult results = validator.Validate(body);

            if(!results.IsValid) 
            {
                var errors = _ValidationErrorBuilder.Generate<AuthEntityLoginBody>(results.Errors, "json");
                
                return new _Response(this)
                            .WithCode(HttpStatusCode.BadRequest)
                            .WithError(errors)
                            .Json();
            }

            return new _Response(this).Json();
        }

        [HttpPost("refresh-token")]
        public ActionResult PostRefreshToken()
        {
            return new _Response(this).Json();
        }

        [HttpPost("forgot-password")]
        public ActionResult PostForgotPassword()
        {
            return new _Response(this).Json();
        }
    }
}