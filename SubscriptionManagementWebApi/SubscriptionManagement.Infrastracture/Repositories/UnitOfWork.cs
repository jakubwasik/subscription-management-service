using SubscriptionManagement.Domain.Repositories;
using SubscriptionManagement.Infrastructure.EntityConfiguration;

namespace SubscriptionManagement.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public ISubscriptionRepository SubscriptionRepository { get; }
    public IUserRepository UserRepository { get; }
    private bool _disposed;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        SubscriptionRepository = new SubscriptionRepository(context);
        UserRepository = new UserRepository(context);
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}