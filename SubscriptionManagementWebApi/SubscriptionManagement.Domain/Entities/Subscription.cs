namespace SubscriptionManagement.Domain.Entities;

public class Subscription
{
    public int Id { get; set; }
    public SubscriptionType SubscriptionType { get; set; }
    public bool IsActive => ActiveTo > DateTimeOffset.UtcNow;
    public DateTimeOffset LastActivated { get; set; }
    public DateTimeOffset ActiveTo { get; set; }
    public bool AutoRenewal { get; set; }

    public void Activate()
    {
        AutoRenewal = true;
        if (!IsActive)
        {
            ActiveTo = DateTimeOffset.UtcNow.AddMonths(1);
        }
    }

    void Stop()
    {
        AutoRenewal = false;
    }
}