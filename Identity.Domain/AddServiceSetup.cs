using Identity.Domain.Interfaces.Event;
using Identity.Domain.Interfaces.User;
using Identity.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Domain;

public static class AddServiceSetup
{
    public static IServiceCollection AddService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IEventPublishService, EventPublishService>();
        return services;
    }
}