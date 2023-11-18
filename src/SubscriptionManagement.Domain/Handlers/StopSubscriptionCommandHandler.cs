using MediatR;
using SubscriptionManagement.Domain.Commands;

namespace SubscriptionManagement.Domain.Handlers;

public class StopSubscriptionCommandHandler : IRequestHandler<StopSubscriptionCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public StopSubscriptionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(StopSubscriptionCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);
        if (user == null)
        {
            throw new Exception($"User with id {request.UserId} not found");
        }

        if (user.Subscription == null)
        {
            throw new Exception($"User with id {request.UserId} has no subscription");
        }

        user.PauseSubscription();
        await _unitOfWork.SaveChangesAsync();
    }
}