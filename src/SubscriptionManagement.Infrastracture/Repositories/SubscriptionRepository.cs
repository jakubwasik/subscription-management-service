using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.Domain.UserAggregate;
using SubscriptionManagement.Infrastructure.EntityConfiguration;

namespace SubscriptionManagement.Infrastructure.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly ApplicationDbContext _context;

    public SubscriptionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Subscription?> GetByIdAsync(int subscriptionId)
    {
        return await _context.Subscriptions.FirstOrDefaultAsync(subscription => subscription.Id == subscriptionId);
    }

    public Subscription AddSubscription(Subscription newSubscription)
    {
        return _context.Subscriptions.Add(newSubscription).Entity;
    }

    public void UpdateSubscription(Subscription modifiedSubscription)
    {
        _context.Entry(modifiedSubscription).State = EntityState.Modified;
    }

}