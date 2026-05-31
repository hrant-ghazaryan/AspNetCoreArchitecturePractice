using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductStoreMVC.Data;
using ProductStoreMVC.Models;

namespace ProductStoreMVC.Controllers;

public class CategoriesController : Controller
{
    private readonly AppDbContext _context;
    public CategoriesController(AppDbContext context)
        => _context = context;

    public IActionResult Index()
    {
        var Categories = _context.Categories.ToList();
        return View(Categories);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
        => View();

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (!ModelState.IsValid)
            return View(category);

        _context.Categories.Add(category);

        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        var editCategory = _context.Categories.Find(id);

        if (editCategory == null)
            return NotFound();

        return View(editCategory);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult EditConfirmed(int id, Category category)
    {
        if (!ModelState.IsValid)
            return View("Edit", category);

        var editCategory = _context.Categories.Find(id);
        if (editCategory == null)
            return NotFound();

        editCategory.Name = category.Name;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin")     ]
    public IActionResult Delete(int id)
    {
        var delCategory = _context.Categories.Find(id);
        if (delCategory == null)
            return NotFound();

        return View(delCategory);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteConfirmed(int id)
    {
        var delCategory = _context.Categories.Find(id);
        if (delCategory is null)
            return NotFound();

        _context.Categories.Remove(delCategory);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
