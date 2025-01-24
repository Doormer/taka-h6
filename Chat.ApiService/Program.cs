using Chat.ApiService.Apis;
using Chat.ApiService.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// 简化的 CORS 配置
builder.Services.AddCors();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors();

builder.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseCors( t=> t.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

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

app.MapDefaultEndpoints();

app.Run();