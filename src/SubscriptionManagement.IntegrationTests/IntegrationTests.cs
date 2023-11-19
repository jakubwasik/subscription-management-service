using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using SubscriptionManagement.Api.DTOs;
using SubscriptionManagement.Domain;
using SubscriptionManagement.Domain.UserAggregate;
using SubscriptionManagement.Infrastructure.EntityConfiguration;
using SubscriptionManagement.IntegrationTests.Fixtures;

namespace SubscriptionManagement.IntegrationTests
{
    public class IntegrationTests : IClassFixture<MsSqlFixture>
    {
        private readonly MsSqlFixture _fixture;
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public IntegrationTests(MsSqlFixture fixture)
        {
            _fixture = fixture;
            _factory = new SubscriptionWebApplicationFactory(fixture);
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task ShouldBeAbleToStartSubscriptionIfUserDoesntHaveOne()
        {
            // Arrange
            User retrievedUser;
            // database population could be moved to some helper class
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                var user = new User
                {
                    Name = $"test-{new Random().Next()}",
                    Email = "test@gmail.com"
                };
                retrievedUser = db.Users.Add(user).Entity;
                await db.SaveChangesAsync();
            }

            // Act
            var patchResponse = await _client.PatchAsJsonAsync($"api/v1/users/{retrievedUser.Id}/subscription", new OperationDto() { Type = ActionType.Start });
            var getResponse = await _client.GetAsync($"api/v1/users/{retrievedUser.Id}/subscription");

            // Assert
            patchResponse.EnsureSuccessStatusCode(); // Status Code 200-299
            getResponse.EnsureSuccessStatusCode();
            var subscription = await getResponse.Content.ReadFromJsonAsync<SubscriptionDto>();
            subscription.Should().NotBeNull();
            subscription.IsActive.Should().BeTrue();
            subscription.AutoRenewal.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldBeAbleToStopSubscriptionIfUserAlreadyHaveOneActive()
        {
            // Arrange
            User retrievedUser;
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                var user = new User
                {
                    Name = "test",
                    Email = "test@gmail.com",
                    Subscription = new Subscription(SubscriptionType.Basic) {  AutoRenewal = true }
                };
                retrievedUser = db.Users.Add(user).Entity;
                await db.SaveChangesAsync();
            }

            // Act
            var patchResponse = await _client.PatchAsJsonAsync($"api/v1/users/{retrievedUser.Id}/subscription", new OperationDto { Type = ActionType.Stop });
            var getResponse = await _client.GetAsync($"api/v1/users/{retrievedUser.Id}/subscription");

            // Assert
            patchResponse.EnsureSuccessStatusCode(); // Status Code 200-299
            getResponse.EnsureSuccessStatusCode();
            var subscription = await getResponse.Content.ReadFromJsonAsync<SubscriptionDto>();
            subscription.Should().NotBeNull();
            subscription.IsActive.Should().BeTrue();
            subscription.AutoRenewal.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldBeAbleToStartSubscriptionIfUserAlreadyHaveOneInInactiveState()
        {
            // Arrange
            // Arrange
            User retrievedUser;
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                var user = new User
                {
                    Name = "test",
                    Email = "test@gmail.com",
                    Subscription = new Subscription(SubscriptionType.Basic) { AutoRenewal = false, ActiveTo = DateTimeOffset.MinValue}
                };
                retrievedUser = db.Users.Add(user).Entity;
                await db.SaveChangesAsync();
            }

            // Act
            var patchResponse = await _client.PatchAsJsonAsync($"api/v1/users/{retrievedUser.Id}/subscription", new OperationDto { Type = ActionType.Start });
            var getResponse = await _client.GetAsync($"api/v1/users/{retrievedUser.Id}/subscription");

            // Assert
            patchResponse.EnsureSuccessStatusCode(); // Status Code 200-299
            getResponse.EnsureSuccessStatusCode();
            var subscription = await getResponse.Content.ReadFromJsonAsync<SubscriptionDto>();
            subscription.Should().NotBeNull();
            subscription.AutoRenewal.Should().BeTrue();
        }
    }
}