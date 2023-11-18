namespace SubscriptionManagement.Api.Commands;

public class StartSubscriptionCommand
{
    public Guid UserId { get;  }

    public StartSubscriptionCommand(Guid userId)
    {
        UserId = userId;
    }
}