using SubscriptionManagement.Domain.UserAggregate;

namespace SubscriptionManagement.Domain;

public interface IUnitOfWork: IDisposable
{
    Task SaveChangesAsync();
    ISubscriptionRepository SubscriptionRepository { get; }
    IUserRepository UserRepository { get; }
}