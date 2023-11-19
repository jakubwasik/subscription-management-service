using MediatR;
using SubscriptionManagement.Domain.Queries;
using SubscriptionManagement.Domain.UserAggregate;

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

public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, Subscription?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSubscriptionQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Subscription?> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
    {
        // Note: it's common to use Dapper for read-only queries, for simplicity EF is used here
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId, includeSubscription: true);
        if (user == null)
        {
            throw new Exception($"User with id {request.UserId} not found");
        }

        return user.Subscription;
    }
}