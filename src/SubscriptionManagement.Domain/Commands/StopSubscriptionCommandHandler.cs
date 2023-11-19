using MediatR;

namespace SubscriptionManagement.Domain.Commands;

public class StopSubscriptionCommandHandler : IRequestHandler<StopSubscriptionCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public StopSubscriptionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(StopSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId, includeSubscription: true);
        if (user == null)
        {
            throw new InvalidOperationException($"User with id {request.UserId} not found");
        }

        if (user.Subscription == null)
        {
            throw new InvalidOperationException($"User with id {request.UserId} has no subscription");
        }

        user.PauseSubscription();
        await _unitOfWork.SaveChangesAsync();
    }
}