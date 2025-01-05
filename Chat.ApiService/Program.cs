using Chat.ApiService.Apis;
using Chat.ApiService.Extensions;
using Microsoft.Extensions.Configuration;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
    builder.Configuration.AddUserSecrets<Program>();
#endif

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

var log = new LoggerConfiguration()
          .WriteTo.OpenTelemetry()
          .CreateLogger();

app.MapOrdersApiV1();

app.MapDefaultEndpoints();

app.Run();

