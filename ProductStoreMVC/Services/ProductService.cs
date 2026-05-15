using ProductStoreMVC.Models;
using ProductStoreMVC.Repositories;

namespace ProductStoreMVC.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
        => _productRepository = productRepository;

    // CREATE
    public async Task<Result> AddAsync(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            return Result.Fail("Name is required");

        if (product.Price <= 0)
            return Result.Fail("Price must be greater than 0");

        await _productRepository.AddAsync(product);
        await _productRepository.SaveAsync();

        return Result.Success();
    }

    // READ ALL
    public async Task<List<Product>> GetAllAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    // READ BY ID
    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    // UPDATE
    public async Task<Result> UpdateAsync(int id, Product product)
    {
        var existing = await _productRepository.GetByIdAsync(id);

        if (existing is null)
            return Result.Fail("Product not found");

        if (string.IsNullOrWhiteSpace(product.Name))
            return Result.Fail("Name is required");

        if (product.Price <= 0)
            return Result.Fail("Price must be greater than 0");

        await _productRepository.UpdateAsync(id, product);
        await _productRepository.SaveAsync();

        return Result.Success();
    }

    // DELETE
    public async Task<Result> DeleteAsync(int id)
    {
        var existing = await _productRepository.GetByIdAsync(id);

        if (existing is null)
            return Result.Fail("Product not found");

        await _productRepository.DeleteAsync(id);
        await _productRepository.SaveAsync();

        return Result.Success();
    }
}