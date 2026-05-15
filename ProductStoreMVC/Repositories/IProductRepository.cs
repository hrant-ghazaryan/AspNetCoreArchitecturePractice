using ProductStoreMVC.Models;

namespace ProductStoreMVC.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(int id);

    Task<List<Product>> GetAllAsync();

    Task AddAsync(Product product);

    Task DeleteAsync(int id);

    Task UpdateAsync(int id, Product product);

    Task SaveAsync();
}