using Chat.Api.Middlewares;
using Chat.Infra;
using Microsoft.OpenApi.Models;

/// <summary>
/// Main entry point of the application
/// Configures and starts the web application
/// </summary>

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

/// <summary>
/// Configure Swagger/OpenAPI documentation
/// </summary>
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Chat API", 
        Version = "v1"
    });
});

/// <summary>
/// Configure CORS policy
/// </summary>
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

/// <summary>
/// Configure database connection
/// </summary>
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}
builder.Services.AddInfrastructure(connectionString);

var app = builder.Build();

/// <summary>
/// Configure the HTTP request pipeline
/// </summary>
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();