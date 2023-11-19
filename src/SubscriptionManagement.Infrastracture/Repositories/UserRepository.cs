using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.Domain.UserAggregate;
using SubscriptionManagement.Infrastructure.EntityConfiguration;

namespace SubscriptionManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int userId, bool includeSubscription = false)
        {
            IQueryable<User> query = _context.Users;
            if (includeSubscription)
            {
                query = query.Include(user => user.Subscription);
            }
            return await query.FirstOrDefaultAsync(user => user.Id == userId);
        }

        public User AddUser(User newUser)
        {
            return _context.Users.Add(newUser).Entity;
        }

        public void UpdateUser(User modifiedUser)
        {
            _context.Entry(modifiedUser).State = EntityState.Modified;
        }
    }
}