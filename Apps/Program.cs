using Apps.Data.Ctx;
using Apps.Repositories;
using Apps.Repositories.Interfaces;
using Apps.Services;
using Apps.Services.Interfaces;
using Apps.Config;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DI Config
// Learn more about getting value from appsettings.json in .NET Core at https://www.telerik.com/blogs/how-to-get-values-from-appsettings-json-in-net-core
builder.Services.Configure<AppCfg>(builder.Configuration.GetSection("App"));
builder.Services.Configure<DatabaseCfg>(builder.Configuration.GetSection("App:Database"));
builder.Services.Configure<MailerCfg>(builder.Configuration.GetSection("App:Mailer"));

// DI DbContext
builder.Services.AddDbContext<TodoContext>();
builder.Services.AddDbContext<UserContext>();

// DI Repositories
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// DI Services
builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<IUserService, UserService>();

// DI Controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();

    app.UseSwagger(options =>
    {
        options.RouteTemplate = "openapi/{documentName}.json";
    });
    app.MapScalarApiReference(options =>
    {
        options.Title = "StarterKit API";
        options.Theme = ScalarTheme.Moon;
        options.ShowSidebar = true;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
        options.EndpointPathPrefix = "/apidocs/{documentName}";
        // options.Authentication = new ScalarAuthenticationOptions
        // {
        //     PreferredSecurityScheme = "ApiKey",
        //     ApiKey = new ApiKeyOptions
        //     {
        //         Token = "my-api-key"
        //     }
        // };
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
