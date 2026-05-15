using ProductStoreMVC.Data;
using ProductStoreMVC.Models;

namespace ProductStoreMVC.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context)
       => _context = context;

    public void Add(Product product)
        => _context.Products.Add(product);

    public void Delete(int id)
    {
        var delProduct = _context.Products.Find(id);

        if (delProduct is null)
            return;

        _context.Products.Remove(delProduct);
    }
    public IEnumerable<Product> GetAll()
        => _context.Products;

    public Product? GetById(int id)
        => _context.Products.Find(id);

    public void Save()
        => _context.SaveChanges();

    public void Update(int id, Product product)
    {
        var oldProduct = _context.Products.Find(id);

        if (oldProduct is null)
            return;

        oldProduct.Name = product.Name;
        oldProduct.Price = product.Price;
        oldProduct.CategoryId = product.CategoryId;
    }
}
