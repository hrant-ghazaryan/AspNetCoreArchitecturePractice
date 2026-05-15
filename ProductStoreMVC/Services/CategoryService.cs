using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProductStoreMVC.Models;
using ProductStoreMVC.Repositories;
using static Microsoft.CodeAnalysis.CSharp.SyntaxTokenParser;

namespace ProductStoreMVC.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
        => _categoryRepository = categoryRepository;

    public async Task<Result> AddAsync(Category category)
    {
        if (String.IsNullOrEmpty(category.Name))
            return Result.Fail("Name is required");

        _categoryRepository.Add(category);
        _categoryRepository.Save();

        return Result.Success();
    }

    public async Task DeleteAsync(int id)
    {
        _categoryRepository.Delete(id);
        _categoryRepository.Save();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task SaveAsync()
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(int id, Category category)
    {
        throw new NotImplementedException();
    }
}
