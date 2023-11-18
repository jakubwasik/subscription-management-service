namespace SubscriptionManagement.Domain.UserAggregate;

public interface ISubscriptionRepository
{
    Task<Subscription?> GetByIdAsync(int subscriptionId);
    Subscription AddSubscription(Subscription newSubscription);
    void UpdateSubscription(Subscription modifiedSubscription);
}