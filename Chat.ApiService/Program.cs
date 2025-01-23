using Chat.ApiService; // 确保包含 ChatHub 所在的命名空间
using Chat.ApiService.Apis;
using Chat.ApiService.Extensions;
using Microsoft.AspNetCore.SignalR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddSignalR(); // 添加 SignalR 服务

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

// Map SignalR Hub
app.MapHub<ChatHub>("/chatHub"); // 这里直接引用 ChatHub，因为在同一个命名空间中

app.MapChatApiV1();
app.MapDefaultEndpoints();

app.Run();