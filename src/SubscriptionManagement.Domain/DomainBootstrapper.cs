using Microsoft.Extensions.DependencyInjection;
using SubscriptionManagement.Domain.Commands;

namespace SubscriptionManagement.Domain;

public static class DomainBootstrapper
{
    public static IServiceCollection RegisterDomainTypes(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining(typeof(StartSubscriptionCommandHandler));
        });
        return services;
    }
}