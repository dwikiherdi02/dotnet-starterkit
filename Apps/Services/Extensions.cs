using Apps.Services.Interfaces;

namespace Apps.Services
{
    public static class Extensions
    {
        public static WebApplicationBuilder UseServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITodoService, TodoService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            return builder;
        }
    }
}