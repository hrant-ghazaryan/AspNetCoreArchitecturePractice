using ProductStoreMVC.Models;

namespace ProductStoreMVC.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUserNameAsync(string userName);

    Task AddAsync(User user);

    Task SaveAsync();
}
