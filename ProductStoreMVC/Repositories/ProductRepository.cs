using Microsoft.EntityFrameworkCore;
using ProductStoreMVC.Data;
using ProductStoreMVC.Models;

namespace ProductStoreMVC.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
        => _context = context;

    // CREATE
    public async Task AddAsync(Product product)
        => await _context.Products.AddAsync(product);

    // READ ALL
    public async Task<List<Product>> GetAllAsync()
        => await _context.Products.ToListAsync();

    // READ BY ID
    public async Task<Product?> GetByIdAsync(int id)
        => await _context.Products.FindAsync(id);

    // UPDATE
    public async Task UpdateAsync(int id, Product product)
    {
        var oldProduct = await _context.Products.FindAsync(id);

        if (oldProduct is null)
            return;

        oldProduct.Name = product.Name;
        oldProduct.Price = product.Price;
        oldProduct.CategoryId = product.CategoryId;
    }

    // DELETE
    public async Task DeleteAsync(int id)
    {
        var delProduct = await _context.Products.FindAsync(id);

        if (delProduct is null)
            return;

        _context.Products.Remove(delProduct);
    }

    // SAVE
    public async Task SaveAsync()
        => await _context.SaveChangesAsync();
}