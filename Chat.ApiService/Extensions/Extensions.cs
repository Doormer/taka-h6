using Chat.Domain.AggregateModels.ChatAggregate;
using Chat.Infra;

namespace Chat.ApiService.Extensions;

public static class Extensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ChatContext>();
        return services;
    }
}