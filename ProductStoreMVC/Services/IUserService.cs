using ProductStoreMVC.Models;

namespace ProductStoreMVC.Services
{
    public interface IUserService
    {
        Task<User?> GetByUserNameAsync(string userName);
        Task<Result> RegisterAsync(User user);
        Task<Result> LoginAsync(string userName, string password);
    }
}
