using MediatR;
using SubscriptionManagement.Domain.UserAggregate;

namespace SubscriptionManagement.Domain.Queries;

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
            throw new InvalidOperationException($"User with id {request.UserId} not found");
        }

        return user.Subscription;
    }
}