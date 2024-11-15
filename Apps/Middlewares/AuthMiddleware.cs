using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Apps.Config;
using Apps.Data.Entities;
using Apps.Data.Models;
using Apps.Middlewares.Attributes;
using Apps.Repositories.Interfaces;
using Apps.Utilities._JwtGenerator;
using Apps.Utilities._Mapper;
using Apps.Utilities._Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Apps.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        
        private readonly JwtCfg _jwtCfg;

        private readonly IServiceProvider _serviceProvider;

        public AuthMiddleware(
            RequestDelegate next,
            IOptions<JwtCfg> jwtCfg,
            IServiceProvider serviceProvider
        )
        {
            _next = next;
            _jwtCfg = jwtCfg.Value;
            _serviceProvider = serviceProvider;
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

                var sessId = claimsPrincipal!.FindFirst(x => x.Type == "sid")?.Value;
                
                var user = await AuthenticatedUser(Ulid.Parse(sessId));
                
                if (sessId == null || user == null)
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

                AuthEntityUserContext authUserCtx = _Mapper.Map<User, AuthEntityUserContext>(user);
                
                context.Items["auth_user"] = authUserCtx;
            }

            await _next(context);
        }

        private bool IsValidToken(string token, out ClaimsPrincipal? claimsPrincipal)
        {
            bool isValid = new _JwtGenerator()
                                    .AddValidateParamters(new TokenValidationParameters{
                                        ValidateIssuerSigningKey = true,
                                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtCfg.Secret)),
                                        ValidateIssuer = false,
                                        ValidateAudience = false,
                                        ClockSkew = TimeSpan.Zero
                                    })
                                    .Validate(token!, out claimsPrincipal);
            
            if (!isValid)
            {
                return false;
            }

            var type = claimsPrincipal!.FindFirst(x => x.Type == "typ")?.Value;

            if (type != "access")
            {
                return false;
            }

            return true;
        }

        private async Task<User?> AuthenticatedUser(Ulid sessionId)
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var authRepo  = scope.ServiceProvider.GetRequiredService<IAuthRepository>();
                
                var session = await authRepo.FindSessionById(sessionId);

                if (session == null)
                {
                    return null;
                }

                /* if (DateTime.UtcNow > session!.ExpiredAt)
                {
                    Console.WriteLine("Masuk sini");
                    return null;
                } */

                return session.User;
            }
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