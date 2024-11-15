using System.Net;
using Apps.Data.Entities;
using Apps.Data.Entities.Rules;
using Apps.Data.Models;
using Apps.Exceptions;
using Apps.Services.Interfaces;
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
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        } 
        
        [HttpPost("login")]
        public async Task<ActionResult> PostLogin([FromBody] AuthEntityLoginBody body)
        {   
            AuthEntityLoginBodyRule validator = new AuthEntityLoginBodyRule();
            ValidationResult results = validator.Validate(body);

            if(!results.IsValid) 
            {
                var errors = _ValidationErrorBuilder.Generate<AuthEntityLoginBody>(results.Errors, "json");
                
                return new _Response(this)
                            .WithCode(HttpStatusCode.BadRequest)
                            .WithErrors(errors)
                            .Json();
            }

            try
            {
                User user = await _authService.Login(body);

                var token = await _authService.GenerateToken(user);

                return new _Response(this)
                            .WithResult(token)
                            .Json();
            }
            catch (HttpResponseException e)
            {
                return new _Response(this, e.StatusCode).WithError(e.Message).Json();
            }
            catch (Exception e)
            {
                return new _Response(this, HttpStatusCode.BadRequest).WithError(e.Message).Json();
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> PostRefreshToken([FromBody] AuthEntityRefreshTokenBody body)
        {
            AuthEntityRefreshTokenBodyRule validator = new AuthEntityRefreshTokenBodyRule();
            ValidationResult results = validator.Validate(body);

            if(!results.IsValid) 
            {
                var errors = _ValidationErrorBuilder.Generate<AuthEntityLoginBody>(results.Errors, "json");
                
                return new _Response(this)
                            .WithCode(HttpStatusCode.BadRequest)
                            .WithErrors(errors)
                            .Json();
            }

            try
            {
                var token = await _authService.RefreshToken(body.RefreshToken);

                return new _Response(this)
                            .WithResult(token)
                            .Json();
            }
            catch (HttpResponseException e)
            {
                return new _Response(this, e.StatusCode).WithError(e.Message).Json();
            }
            catch  (Exception e)
            {
                return new _Response(this, HttpStatusCode.BadRequest).WithError(e.Message).Json();
            }
        }

        /* [HttpPost("forgot-password")]
        public ActionResult PostForgotPassword()
        {
            return new _Response(this).Json();
        } */
    }
}