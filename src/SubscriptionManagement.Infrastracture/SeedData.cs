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
        var names = new List<string> { "John", "Jane", "Jack", "Jill" };
        if (!ctx.Users.Any())
        {
            var users = names.Select(x =>
            {
                var user = new User
                {
                    Name = x, Email = $"{x}@gmail.com"
                };
                return user;
            }).ToList();

            users[0].AddSubscription(SubscriptionType.Basic);
            users[0].ActivateSubscription();
            users[1].AddSubscription(SubscriptionType.Basic);
            users[1].ActivateSubscription();
            users[1].PauseSubscription();
            ctx.Users.AddRange(users);
            await ctx.SaveChangesAsync();
        }

        //var u = ctx.Users.Find(1);
        //u.AddSubscription(SubscriptionType.Premium);
        //u.ActivateSubscription();
        //await ctx.SaveChangesAsync();
    }
}