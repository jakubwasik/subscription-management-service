using System.Text.Json.Serialization;
using SubscriptionManagement.Domain.UserAggregate;

namespace SubscriptionManagement.Api.DTOs;

public class SubscriptionDto
{
    public bool IsActive { get; set; }
    public bool AutoRenewal { get; set; }
    public string SubscriptionType { get; set; }
    public DateTime ActiveTo { get; set; }

    public SubscriptionDto(Subscription subscription)
    {
        IsActive = subscription.IsActive;
        AutoRenewal = subscription.AutoRenewal;
        SubscriptionType = subscription.SubscriptionType.ToString();
        ActiveTo = subscription.ActiveTo.LocalDateTime;
    }

    [JsonConstructor]
    public SubscriptionDto(bool isActive, bool autoRenewal, string subscriptionType, DateTime activeTo)
    {
        IsActive = isActive;
        AutoRenewal = autoRenewal;
        SubscriptionType = subscriptionType;
        ActiveTo = activeTo;
    }
}