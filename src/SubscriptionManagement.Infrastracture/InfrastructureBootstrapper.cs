using Microsoft.Extensions.DependencyInjection;
using SubscriptionManagement.Domain;
using SubscriptionManagement.Domain.UserAggregate;
using SubscriptionManagement.Infrastructure.Repositories;

namespace SubscriptionManagement.Infrastructure;

public static class InfrastructureBootstrapper
{
    public static IServiceCollection RegisterInfrastructureTypes(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}