namespace SubscriptionManagement.Domain.UserAggregate;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int userId, bool includeSubscription = false);
    User AddUser(User newUser);
    void UpdateUser(User modifiedUser);
}