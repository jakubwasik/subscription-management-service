using System.Text.Json.Serialization;
using SubscriptionManagement.Domain.UserAggregate;

namespace SubscriptionManagement.Api.DTOs;

public class SubscriptionDto
{
    public bool IsActive { get; set; }
    public bool AutoRenewal { get; set; }
    public string SubscriptionType { get; set; }
    public DateTime ActiveTo { get; set; }
}