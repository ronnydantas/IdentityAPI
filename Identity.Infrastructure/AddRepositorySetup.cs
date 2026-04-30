using Identity.Domain.Interfaces.Event;
using Identity.Domain.Interfaces.User;
using Identity.Infrastructure.Context;
using Identity.Infrastructure.RabbitMQ;
using Identity.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;

public static class AddRepositorySetup
{
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            // Use the same connection string name as in appsettings.json (DefaultConnection)
            services.AddDbContext<PostgresContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRabbitMqPublisher, RabbitMqPublisher>();
        return services;
        }
}