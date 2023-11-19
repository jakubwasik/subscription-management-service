using Microsoft.EntityFrameworkCore.Query.Internal;
using SubscriptionManagement.Domain;
using SubscriptionManagement.Domain.UserAggregate;
using SubscriptionManagement.Infrastructure.EntityConfiguration;

namespace SubscriptionManagement.Infrastructure;

/// <summary>
/// Seed database with some random data
/// </summary>
public class SeedData
{
    public async Task SeedAsync(ApplicationDbContext ctx)
    {
        if (!ctx.Users.Any())
        {
            AddUserWithActiveSubscription(ctx, "John");
            AddUserWithActiveSubscriptionWithoutAutoRenewal(ctx, "Jane");
            AddUserWithoutSubscription(ctx, "Jack");
            await ctx.SaveChangesAsync();
        }
    }

    private static void AddUserWithActiveSubscription(ApplicationDbContext ctx, string username)
    {
        var user = new User
        {
            Name = username,
            Email = $"{username}@gmail.com",
        };
        user.AddSubscription(SubscriptionType.Basic);
        user.ActivateSubscription();
        ctx.Users.Add(user);
    }

    private static void AddUserWithActiveSubscriptionWithoutAutoRenewal(ApplicationDbContext ctx, string username)
    {
        var user = new User
        {
            Name = username,
            Email = $"{username}@gmail.com",
        };
        user.AddSubscription(SubscriptionType.Basic);
        user.ActivateSubscription();
        user.PauseSubscription();
        ctx.Users.Add(user);
    }

    private static void AddUserWithoutSubscription(ApplicationDbContext ctx, string username)
    {
        var user = new User
        {
            Name = username,
            Email = $"{username}@gmail.com",
        };
        ctx.Users.Add(user);
    }
}