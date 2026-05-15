using Microsoft.EntityFrameworkCore;
using ProductStoreMVC.Data;
using ProductStoreMVC.Models;

namespace ProductStoreMVC.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
        => _context = context;

    // CREATE
    public async Task AddAsync(Category category)
        => await _context.Categories.AddAsync(category);

    // READ ALL
    public async Task<List<Category>> GetAllAsync()
        => await _context.Categories.ToListAsync();

    // READ BY ID
    public async Task<Category?> GetByIdAsync(int id)
        => await _context.Categories.FindAsync(id);

    // UPDATE
    public async Task UpdateAsync(int id, Category category)
    {
        var oldCategory = await _context.Categories.FindAsync(id);

        if (oldCategory is null)
            return;

        oldCategory.Name = category.Name;
    }

    // DELETE
    public async Task DeleteAsync(int id)
    {
        var delCategory = await _context.Categories.FindAsync(id);

        if (delCategory is null)
            return;

        _context.Categories.Remove(delCategory);
    }

    // SAVE
    public async Task SaveAsync()
        => await _context.SaveChangesAsync();
}