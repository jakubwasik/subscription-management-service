using MediatR;

namespace SubscriptionManagement.Domain.Commands;

public class StartSubscriptionCommand : IRequest
{
    public int UserId { get; }

    public StartSubscriptionCommand(int userId)
    {
        UserId = userId;
    }
}