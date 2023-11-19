namespace SubscriptionManagement.Domain.UserAggregate
{
    /// <summary>
    /// User Aggregate Root Entity, entry point for Subscription.
    /// todo: this class should be better encapsulated
    /// </summary>
    public class User
    {
        public int Id { get; init; }
        public string Name { get; set; } = string.Empty;
        public required string Email { get; init; } = string.Empty;
        public Subscription? Subscription { get; set; }

        public Subscription AddSubscription(SubscriptionType subscriptionType)
        {
            if (Subscription != null)
            {
                throw new InvalidOperationException("Subscription already exists");
            }

            Subscription = new Subscription(subscriptionType);
            return Subscription;
        }

        public void PauseSubscription()
        {
            if (Subscription == null)
            {
                throw new InvalidOperationException("Subscription does not exist");
            }

            Subscription.Stop();
        }

        public void ActivateSubscription()
        {
            if (Subscription == null)
            {
                throw new InvalidOperationException("You need to create subscription first");
            }

            Subscription.Start();
        }
    }
}