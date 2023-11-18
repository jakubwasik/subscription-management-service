using MediatR;

namespace SubscriptionManagement.Domain.Commands;

public class StopSubscriptionCommand : IRequest
{
    public int UserId { get; }

    public StopSubscriptionCommand(int userId)
    {
        UserId = userId;
    }
}