using Microsoft.IdentityModel.Tokens;
using ProductStoreMVC.Models;
using ProductStoreMVC.Repositories;

namespace ProductStoreMVC.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
        => _userRepository = userRepository;

    public async Task<Result> RegisterAsync(User user)
    {
        if (user is null)
            return Result.Fail("Incorrect user data");

        if (string.IsNullOrWhiteSpace(user.Name))
            return Result.Fail("Name is required");

        if (string.IsNullOrWhiteSpace(user.PasswordHesh))
            return Result.Fail("Password is required");

        var existingUser = await _userRepository.GetByUserNameAsync(user.Name);
        if (existingUser is not null)
            return Result.Fail("Please choose another name");

        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();
        return Result.Success();
    }
    public async Task<Result> LoginAsync(string userName, string password)
    {
        var existingUser = await _userRepository.GetByUserNameAsync(userName);
        if (existingUser is null)
             return Result.Fail("User not found");
        
        return Result.Success();
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            return null;

        return await _userRepository.GetByUserNameAsync(userName);
    }
}
