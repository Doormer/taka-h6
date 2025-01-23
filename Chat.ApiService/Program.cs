using Chat.ApiService; // 确保包含 ChatHub 所在的命名空间
using Chat.ApiService.Apis;
using Chat.ApiService.Extensions;
using Chat.Domain.AggregateModels.MessageAggregate;
using Chat.Infra.Repositories;
using Microsoft.AspNetCore.SignalR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddSignalR(); // 添加 SignalR 服务

// 注册消息仓储
builder.Services.AddScoped<IMessageRepo, MessageRepo>();

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

// Map SignalR Hub
app.MapHub<ChatHub>("/chatHub");

app.MapChatApiV1();
app.MapDefaultEndpoints();

app.Run();