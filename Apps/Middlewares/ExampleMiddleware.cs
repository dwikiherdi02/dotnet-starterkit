

using Apps.Middlewares.Attributes;

namespace Apps.Middlewares
{
    public class ExampleMiddleware
    {
        private readonly RequestDelegate _next;

        public ExampleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var attribute = endpoint?.Metadata.GetMetadata<ExampleMiddlewareAttribute>();
            if (attribute != null)
            {
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine($"Example middleware loaded on {attribute.Name}");
            }

            await _next(context);
        }

    }

    public static class ExampleMiddlewareExtensions
    {
        public static IApplicationBuilder UseExampleMiddleware(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExampleMiddleware>();
        }
    }
}