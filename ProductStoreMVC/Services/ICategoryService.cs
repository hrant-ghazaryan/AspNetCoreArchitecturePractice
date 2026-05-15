using ProductStoreMVC.Models;

namespace ProductStoreMVC.Services;

public interface ICategoryService
{
    Task<Category?> GetByIdAsync(int id);

    Task<IEnumerable<Category>> GetAllAsync();

    Task<Result> AddAsync(Category category);

    Task<Result> DeleteAsync(int id);

    Task<Result> UpdateAsync(int id, Category category);
}