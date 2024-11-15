using Apps.Data.Ctx;
using Apps.Repositories;
using Apps.Services;
using Apps.Config;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Apps.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthorization();

// DI HttpContext
builder.Services.AddHttpContextAccessor();

// DI Config
builder.UseConfig();

// DI DbContext
builder.UseDbContext();

// DI Repositories
builder.UseRepositories();

// DI Services
builder.UseServices();

// DI Controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // options.JsonSerializerOptions.Converters.Add(new _UlidJsonConveter());
        
        // Set agar property bernilai null tidak disertakan dalam JSON
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;

        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;

    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger => {
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        // Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });

    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}

                }
            });
});

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
        options.EndpointPathPrefix = "/docs/{documentName}";
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

app.UseAuthMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
