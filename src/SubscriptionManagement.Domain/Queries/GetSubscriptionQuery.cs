using MediatR;
using SubscriptionManagement.Domain.UserAggregate;

namespace SubscriptionManagement.Domain.Queries;

public class GetSubscriptionQuery : IRequest<Subscription?>
{
    public int UserId { get; }

    public GetSubscriptionQuery(int userId)
    {
        UserId = userId;
    }
}