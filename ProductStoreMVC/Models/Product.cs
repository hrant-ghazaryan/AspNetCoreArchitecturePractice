using System.ComponentModel.DataAnnotations;

namespace ProductStoreMVC.Models;

public class Product
{
    // Primary Key
    public int Id { get; set; }
    [Required(ErrorMessage = "Product Name is required")]
    [MinLength(2,ErrorMessage = "Minimum 2 characters")]
    [MaxLength(50 , ErrorMessage = "Maximum 50 characters")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [Range(1,10_000_000,
        ErrorMessage = "Price must be between 1 and 10_000_000")]
    public decimal Price { get; set; }

    // FOREIGN KEY
    [Required(ErrorMessage = "Category is required")]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

}
