namespace SubscriptionManagement.Domain.UserAggregate;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int userId);
    User AddUser(User newUser);
    void UpdateUser(User modifiedUser);
}