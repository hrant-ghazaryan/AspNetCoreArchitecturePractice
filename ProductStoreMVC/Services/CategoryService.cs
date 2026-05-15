using ProductStoreMVC.Models;
using ProductStoreMVC.Repositories;

namespace ProductStoreMVC.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
        => _categoryRepository = categoryRepository;

    // CREATE
    public async Task<Result> AddAsync(Category category)
    {
        if (string.IsNullOrWhiteSpace(category.Name))
            return Result.Fail("Name is required");

        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveAsync();

        return Result.Success();
    }

    // DELETE
    public async Task<Result> DeleteAsync(int id)
    {
        var existing = await _categoryRepository.GetByIdAsync(id);

        if (existing is null)
            return Result.Fail("Category not found");

        await _categoryRepository.DeleteAsync(id);
        await _categoryRepository.SaveAsync();

        return Result.Success();
    }

    // READ ALL
    public async Task<IEnumerable<Category>> GetAllAsync()
        => await _categoryRepository.GetAllAsync();

    // READ BY ID
    public async Task<Category?> GetByIdAsync(int id)
        => await _categoryRepository.GetByIdAsync(id);

    // UPDATE
    public async Task<Result> UpdateAsync(int id, Category category)
    {
        var existing = await _categoryRepository.GetByIdAsync(id);

        if (existing is null)
            return Result.Fail("Category not found");

        if (string.IsNullOrWhiteSpace(category.Name))
            return Result.Fail("Name is required");

        await _categoryRepository.UpdateAsync(id, category);
        await _categoryRepository.SaveAsync();

        return Result.Success();
    }
}