using ProductStoreMVC.Models;

namespace ProductStoreMVC.Services;

public interface IProductService
{
    Task<Product?> GetByIdAsync(int id);

    Task<List<Product>> GetAllAsync();

    Task<Result> AddAsync(Product product);

    Task<Result> DeleteAsync(int id);

    Task<Result> UpdateAsync(int id, Product product);
}