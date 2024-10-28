using Apps.Data.Ctx;
using Apps.Repositories;
using Apps.Services;
using Apps.Config;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DI Config
builder.UseConfig();

// DI DbContext
builder.UseDbContext();

// DI Repositories
builder.UseRepositories();

// DI Services
builder.UseServices();

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
