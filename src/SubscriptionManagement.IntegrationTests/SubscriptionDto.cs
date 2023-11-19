internal class SubscriptionDto
{
    public bool IsActive { get; }
    public bool AutoRenewal { get; }
    public string SubscriptionType { get; }
    public DateTime ActiveTo { get; }
}