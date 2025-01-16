using Chat.Domain.AggregateModels.MessageAggregate;
using Chat.Domain.AggregateModels.ReciprocalContactAggregate;
using Chat.Domain.SeedWork;
using Chat.Infra.Persistence;
using Chat.Infra.Repositories;
using Chat.Infra.Idempotency;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace Chat.Infra.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TakaDB")
            ?? throw new InvalidOperationException("Connection string 'TakaDB' not found.");
            
        services.AddDbContext<ChatContext>(options =>
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                x => x.MigrationsAssembly("Chat.Infra")
            ));

        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ChatContext>());

        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IReciprocalContactRepo, ReciprocalContactRepo>();
        services.AddScoped<IRequestManager, RequestManager>();

        return services;
    }
} 