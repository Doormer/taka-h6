using Chat.Application.Common.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace Chat.Application.Extensions;

public static class ExceptionHandlingExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
} 