namespace SubscriptionManagement.Domain.UserAggregate;

public class Subscription
{
    private const int SubscriptionPeriod = 1;

    public Subscription(SubscriptionType subscriptionType)
    {
        SubscriptionType = subscriptionType;
        AutoRenewal = true;
        ActiveTo = DateTimeOffset.UtcNow.AddMonths(SubscriptionPeriod);
    }

    public int Id { get; set; }
    public SubscriptionType SubscriptionType { get; set; }
    public bool IsActive => ActiveTo > DateTimeOffset.UtcNow;
    public DateTimeOffset ActiveTo { get; set; }
    public bool AutoRenewal { get; set; }

    public void Start()
    {
        AutoRenewal = true;
        if (!IsActive)
        {
            // there should be an event sent to the payment service to charge the user
        }
    }

    public void Stop()
    {
        AutoRenewal = false;
    }

    /// <summary>
    /// Method to be used by the background service to check if subscription should be renewed
    /// </summary>
    public bool ShouldBeRenewed()
    {
        return AutoRenewal && !IsActive;
    }

    /// <summary>
    /// Method to be called in response to payment service
    /// event that subscription has been paid
    /// </summary>
    public void Renew()
    {
        ActiveTo = ActiveTo.AddMonths(SubscriptionPeriod);
    }
}