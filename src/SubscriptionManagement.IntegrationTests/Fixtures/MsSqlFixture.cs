using Testcontainers.MsSql;

namespace SubscriptionManagement.IntegrationTests.Fixtures;

public sealed class MsSqlFixture : IAsyncLifetime
{
    public MsSqlContainer MsSqlContainer { get; } = new MsSqlBuilder().Build();

    public Task InitializeAsync()
    {
        return MsSqlContainer.StartAsync();
    }

    public Task DisposeAsync()
    {
        return MsSqlContainer.DisposeAsync().AsTask();
    }
}