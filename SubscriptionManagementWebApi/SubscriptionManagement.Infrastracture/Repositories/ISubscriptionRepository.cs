using SubscriptionManagement.Domain.Entities;

namespace SubscriptionManagement.Infrastructure.Repositories;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByIdAsync(int subscriptionId);
    Subscription AddSubscription(Subscription newSubscription);
    void UpdateSubscription(Subscription modifiedSubscription);
}