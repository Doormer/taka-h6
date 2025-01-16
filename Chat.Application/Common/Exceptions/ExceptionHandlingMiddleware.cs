using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;

namespace Chat.Application.Common.Exceptions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            ChatValidationException => (HttpStatusCode.BadRequest, "验证错误"),
            ChatNotFoundException => (HttpStatusCode.NotFound, "资源未找到"),
            UnauthorizedMessageAccessException => (HttpStatusCode.Forbidden, "无权访问"),
            MessageOperationException => (HttpStatusCode.BadRequest, "操作失败"),
            _ => (HttpStatusCode.InternalServerError, "服务器内部错误")
        };

        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            StatusCode = statusCode,
            Message = message,
            Details = _env.IsDevelopment() ? exception.ToString() : exception.Message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
} 