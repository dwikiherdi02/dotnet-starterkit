namespace Apps.Config
{
    public static class Extensions
    {
        public static WebApplicationBuilder UseConfig(this WebApplicationBuilder builder)
        {
            // Learn more about getting value from appsettings.json in .NET Core at https://www.telerik.com/blogs/how-to-get-values-from-appsettings-json-in-net-core
            
            builder.Services.Configure<AppCfg>(builder.Configuration.GetSection("App"));
            builder.Services.Configure<DatabaseCfg>(builder.Configuration.GetSection("App:Database"));
            builder.Services.Configure<MailerCfg>(builder.Configuration.GetSection("App:Mailer"));
            
            return builder;
        }
    }
}