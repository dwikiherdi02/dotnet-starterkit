using System.Net;
using System.Security.Claims;
using Apps.Config;
using Apps.Middlewares.Attributes;
using Apps.Utilities._JtwGenerator;
using Apps.Utilities._Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Apps.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        
        private readonly JwtCfg _jwtCfg;

        public AuthMiddleware(RequestDelegate next, IOptions<JwtCfg> jwtCfg)
        {
            _next = next;
            _jwtCfg = jwtCfg.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var attribute = endpoint?.Metadata.GetMetadata<AuthMiddlewareAttribute>();
            
            if (attribute != null)
            {
                // var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                ActionResult? response;

                if (token == null || token == "")
                {
                    response = new _Response(context, HttpStatusCode.Unauthorized)
                                        .WithError("Akses dilarang.")
                                        .Json();

                    await response.ExecuteResultAsync(new ActionContext
                    {
                        HttpContext = context
                    });

                    return;
                }

                ClaimsPrincipal? claimsPrincipal;

                if (!IsValidToken(token, out claimsPrincipal))
                {
                    response = new _Response(context, HttpStatusCode.Unauthorized)
                                        .WithError("Token tidak valid.")
                                        .Json();

                    await response.ExecuteResultAsync(new ActionContext
                    {
                        HttpContext = context
                    });
                    
                    return;
                }

                var sessId = claimsPrincipal!.FindFirst(x => x.Type == "sess_id")?.Value;
            }

            await _next(context);
        }

        private bool IsValidToken(string token, out ClaimsPrincipal? claimsPrincipal)
        {
            bool isValid = new _JtwGenerator()
                                    .AddSecret(_jwtCfg.Secret)
                                    .Validate(token!, out claimsPrincipal);
            
            if (!isValid)
            {
                return false;
            }

            var type = claimsPrincipal!.FindFirst(x => x.Type == "type")?.Value;

            if (type != "access")
            {
                return false;
            }

            return true;
        }
    }

    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthMiddleware>();
        }
    }
}