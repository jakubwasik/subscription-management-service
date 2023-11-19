using SubscriptionManagement.Api.DTOs;
using SubscriptionManagement.Domain.UserAggregate;

public static class Mapper
{
    public static SubscriptionDto ToDto(Subscription subscription)
    {
        return new SubscriptionDto
        {
            SubscriptionType = subscription.SubscriptionType.ToString(),
            IsActive = subscription.IsActive,
            ActiveTo = subscription.ActiveTo.LocalDateTime,
            AutoRenewal = subscription.AutoRenewal
        };
    }
}