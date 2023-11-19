using System.Data.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.Infrastructure.EntityConfiguration;

namespace SubscriptionManagement.IntegrationTests.Fixtures;

internal sealed class SubscriptionWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;

    public SubscriptionWebApplicationFactory(MsSqlFixture fixture)
    {
        _connectionString = fixture.MsSqlContainer.GetConnectionString();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Remove(services.SingleOrDefault(service => typeof(DbContextOptions<ApplicationDbContext>) == service.ServiceType));
            services.Remove(services.SingleOrDefault(service => typeof(DbConnection) == service.ServiceType));
            services.AddDbContext<ApplicationDbContext>((_, option) => option.UseSqlServer(_connectionString));
        });
    }
}