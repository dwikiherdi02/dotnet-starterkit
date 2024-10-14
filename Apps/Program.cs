using Apps.Data;
using Apps.Interfaces.Repositories;
using Apps.Interfaces.Services;
using Apps.Repositories;
using Apps.Services;
using Apps.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// DI Settings
// Learn more about getting value from appsettings.json in .NET Core at https://www.telerik.com/blogs/how-to-get-values-from-appsettings-json-in-net-core
// builder.Services.Configure<App>(builder.Configuration.GetSection(""));
builder.Services.Configure<Logging>(builder.Configuration.GetSection("Logging"));
builder.Services.Configure<Database>(builder.Configuration.GetSection("Database"));

// DI DbContext
builder.Services.AddDbContext<TodoContext>();

// DI Repositories
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

// DI Services
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
