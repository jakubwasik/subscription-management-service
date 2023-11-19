using SubscriptionManagement.Domain.UserAggregate;

namespace SubscriptionManagement.Api.DTOs;

public class SubscriptionDto
{
    public bool IsActive { get; }
    public bool AutoRenewal { get; }
    public string SubscriptionType { get; }
    public DateTimeOffset ActiveTo { get; }

    public SubscriptionDto(Subscription subscription)
    {
        IsActive = subscription.IsActive;
        AutoRenewal = subscription.AutoRenewal;
        SubscriptionType = subscription.SubscriptionType.ToString();
        ActiveTo = subscription.ActiveTo.LocalDateTime;
    }
}