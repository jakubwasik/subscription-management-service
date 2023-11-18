using SubscriptionManagement.Domain.Repositories;

namespace SubscriptionManagement.Infrastructure.Repositories;

public interface IUnitOfWork: IDisposable
{
    Task SaveChangesAsync();
    ISubscriptionRepository SubscriptionRepository { get; }
    IUserRepository UserRepository { get; }
}