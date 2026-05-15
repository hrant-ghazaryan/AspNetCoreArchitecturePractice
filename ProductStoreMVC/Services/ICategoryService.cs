using ProductStoreMVC.Models;

namespace ProductStoreMVC.Services;

public interface ICategoryService
{
    public Task<Category?> GetByIdAsync(int id);
    public Task<IEnumerable<Category>> GetAllAsync();

    public Task AddAsync(Category category);
    public Task DeleteAsync(int id);
    public Task UpdateAsync(int id, Category category);

    public Task SaveAsync();
}
