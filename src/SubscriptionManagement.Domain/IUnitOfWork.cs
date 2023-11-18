using SubscriptionManagement.Domain.UserAggregate;

namespace SubscriptionManagement.Domain;

public interface IUnitOfWork: IDisposable
{
    Task SaveChangesAsync();
    IUserRepository UserRepository { get; }
}