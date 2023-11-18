namespace SubscriptionManagement.Domain.UserAggregate
{
    /// <summary>
    /// User Aggregate Root Entity, entry point for Subscription
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
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