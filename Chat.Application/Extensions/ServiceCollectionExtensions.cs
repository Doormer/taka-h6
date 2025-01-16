using MediatR;
using Chat.Application.Behaviors;
using Chat.Application.Common.Caching;
using Chat.Application.Common.Interfaces;
using Chat.Application.Common.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FluentValidation;
using Microsoft.Extensions.Options;
using Chat.Application.Commands.SendMessage;

namespace Chat.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RetryBehavior<,>));
        });

        services.AddMemoryCache();
        services.Configure<CacheConfiguration>(options =>
        {
            var section = configuration.GetSection("Cache");
            options.DefaultExpirationMinutes = section.GetValue<int>("DefaultExpirationMinutes");
            options.MaxCacheItems = section.GetValue<int>("MaxCacheItems");
        });
        services.AddSingleton<ICacheService, MemoryCacheService>();

        services.AddValidatorsFromAssembly(typeof(SendMessageCommand).Assembly);

        return services;
    }
} 