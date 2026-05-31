using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductStoreMVC.Data;
using ProductStoreMVC.Models;

namespace ProductStoreMVC.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
        => _context = context;

    public async Task AddAsync(User user)
        => await _context.Users.AddAsync(user);

    public async Task<User?> GetByUserNameAsync(string userName)
        => await _context.Users.FirstOrDefaultAsync(user => user.Name == userName); 

    public async Task SaveAsync()
        => await _context.SaveChangesAsync();
}
