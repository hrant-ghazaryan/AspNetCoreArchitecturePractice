using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductStoreMVC.Data;
using ProductStoreMVC.Models;
using ProductStoreMVC.ViewModels;

namespace ProductStoreMVC.Controllers;

public class ProductsController : Controller
{
    private readonly AppDbContext _context;
    public ProductsController(AppDbContext context)
        => _context = context;

    public IActionResult Index()
    {
        var Products = _context.Products.
            Include(p => p.Category).
            ToList();
        return View(Products);
    }

    public IActionResult Create()
    {
        var vm = new ProductCreateViewModel
        {
            Categories = _context.Categories.
            Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).
            ToList()
        };

        return View(vm);
    }
    [HttpPost]
    public IActionResult Create(ProductCreateViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();

            return View(vm);
        }

        var product = new Product
        {
            Name = vm.Name,
            Price = vm.Price,
            CategoryId = vm.CategoryId
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        var editProduct = _context.Products.
            FirstOrDefault(p => p.Id == id);

        if (editProduct is null)
            return NotFound();

        var vm = new ProductEditViewModel
        {
            Id = editProduct.Id,
            Name = editProduct.Name,
            Price = editProduct.Price,
            CategoryId = editProduct.CategoryId,

            Categories = _context.Categories.
            Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList()
        };
        ViewBag.Categories = _context.Categories
        .Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        })
        .ToList();

        return View(editProduct);
    }
    [HttpPost]
    public IActionResult EditConfirmed(int id, Product product)
    {
        if (!ModelState.IsValid)
            return View("Edit", product);
        var editProduct = _context.Products.Find(id);
        if (editProduct is null)
            return NotFound();

        editProduct.Name = product.Name;
        editProduct.Price = product.Price;
        editProduct.CategoryId = product.CategoryId;

        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var delProduct = _context.Products.
            Include(p => p.Category).
            FirstOrDefault(p => p.Id == id);

        if (delProduct is null)
            return NotFound();
        return View(delProduct);
    }
    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        var delProduct = _context.Products.Find(id);
        if (delProduct is null)
            return NotFound();

        _context.Products.Remove(delProduct);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
