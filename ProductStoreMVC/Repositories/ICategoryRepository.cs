using ProductStoreMVC.Models;

namespace ProductStoreMVC.Repositories;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(int id);
    Task<List<Category>> GetAllAsync();

    Task AddAsync(Category category);
    Task DeleteAsync(int id);
    Task UpdateAsync(int id, Category category);

    Task SaveAsync();
}
