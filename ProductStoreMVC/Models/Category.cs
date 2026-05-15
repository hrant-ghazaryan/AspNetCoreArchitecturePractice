namespace ProductStoreMVC.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    ICollection<Product>? products { get; set; }
}
