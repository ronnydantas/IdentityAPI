using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Identity.Domain;

public static class AddDomainSetup
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        // Register domain services (IUserService, etc.)
        services.AddService(configuration);

        // Register MediatR handlers from the domain assembly
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AddDomainSetup).Assembly));

        // Required by UserService to access the current HttpContext
        services.AddHttpContextAccessor();

        return services;
    }
}
