using Chat.ApiService.Apis;
using Chat.ApiService.Extensions;
using Chat.Application.Extensions;
using Chat.Infra.Extensions;
using Microsoft.Extensions.Configuration;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

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

app.MapChatApiV1();

app.MapMessageApiV1();

app.MapDefaultEndpoints();

app.Run();

