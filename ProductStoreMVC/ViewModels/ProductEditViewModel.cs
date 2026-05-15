using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProductStoreMVC.ViewModels;

public class ProductEditViewModel
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }

    [Required]
    [Range(1, 10_000_000)]
    public decimal Price { get; set; }

    [Required]
    public int CategoryId { get; set; }

    // DropDown Data
    public List<SelectListItem>? Categories { get; set; }
}
