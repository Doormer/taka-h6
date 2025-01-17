using Chat.ApiService.Apis;
using Chat.ApiService.Extensions;
using Chat.Application.Extensions;
using Chat.Infra.Extensions;
using Chat.Infra;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure URLs
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5598);
})
.UseUrls("http://0.0.0.0:5598");

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Add application layer (包含 MediatR, Validators, 等)
builder.Services.AddApplication(builder.Configuration);

// Add infrastructure layer (包含 DbContext, Repositories, 等)
builder.Services.AddInfrastructure(builder.Configuration);

// Add API layer services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chat API V1");
    });
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ChatContext>();
    context.Database.Migrate();
}

var log = new LoggerConfiguration()
          .WriteTo.OpenTelemetry()
          .CreateLogger();

app.MapChatApiV1();
app.MapMessageApiV1();
app.MapDefaultEndpoints();

app.Run();

