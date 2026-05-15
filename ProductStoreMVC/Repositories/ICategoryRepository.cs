using ProductStoreMVC.Models;

namespace ProductStoreMVC.Repositories;

public interface ICategoryRepository
{
    public Category? GetById(int id);
    public IEnumerable<Category> GetAll();

    public void Add(Category category);
    public void Delete(int id);
    public void Update(int id, Category category);

    public void Save();
}
