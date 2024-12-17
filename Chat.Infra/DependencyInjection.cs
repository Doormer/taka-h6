using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Chat.Domain.AggregatesModel.ContactAggregate;
using Chat.Domain.AggregatesModel.ArchiveAggregate;
using Chat.Domain.Services;
using Chat.Infra.Data;
using Chat.Infra.Repositories;
using Chat.Infra.Services;

namespace Chat.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, 
            string connectionString)
        {
            services.AddDbContext<ChatDbContext>(options =>
                options.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)
                ));

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IArchiveRepository, ArchiveRepository>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
} 