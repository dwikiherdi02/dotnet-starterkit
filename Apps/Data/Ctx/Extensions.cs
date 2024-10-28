namespace Apps.Data.Ctx
{
    public static class Extensions
    {
        public static WebApplicationBuilder UseDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<TodoContext>();
            builder.Services.AddDbContext<UserContext>();
            
            return builder;
        }
    }
}