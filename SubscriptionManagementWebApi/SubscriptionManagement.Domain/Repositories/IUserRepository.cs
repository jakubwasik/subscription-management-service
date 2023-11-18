using System;
using SubscriptionManagement.Domain.Entities;

namespace SubscriptionManagement.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int userId);
    User AddUser(User newUser);
    void UpdateUser(User modifiedUser);
}