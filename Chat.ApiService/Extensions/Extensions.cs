using Chat.Application.Queries;
using Chat.Application.Behaviors;
using Chat.Domain.AggregateModels.ReciprocalContactAggregate;
using Chat.Infra;
using Chat.Infra.Idempotency;
using Chat.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Chat.ApiService.Extensions;

internal static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        // required for minimal API to use Swagger
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        // Pooling is disabled because of the following error:
        // Unhandled exception. System.InvalidOperationException:
        // The DbContext of type 'OrderingContext' cannot be pooled because it does not have a public constructor accepting a single parameter of type DbContextOptions or has more than one constructor.
        services.AddDbContext<ChatContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("TakaDB");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
        builder.EnrichMySqlDbContext<ChatContext>();
        
        services.AddMediatR(cfg =>
        {
            // it registers every handler. No need to add individual handlers
            cfg.RegisterServicesFromAssemblyContaining(typeof(Chat.Application.Commands.AddContactCommandHandler));

            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });

        // Register the command validators for the validator behavior (validators based on FluentValidation library)
        // services.AddSingleton<IValidator<AddContactCommand>, AddContactCommandValidator>();

        services.AddScoped<IReciprocalContactRepo, ReciprocalContactRepo>();
        services.AddScoped<IRequestManager, RequestManager>();
        services.AddScoped<IChatQueries, ChatQueries>();
    }
}