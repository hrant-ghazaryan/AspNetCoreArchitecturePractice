using ProductStoreMVC.Models;

namespace ProductStoreMVC.Repositories;

public interface IProductRepository
{
    public Product? GetById(int id);
    public IEnumerable<Product> GetAll();

    public void Add(Product product);
    public void Delete(int id);
    public void Update(int id, Product product);

    public void Save();
}
