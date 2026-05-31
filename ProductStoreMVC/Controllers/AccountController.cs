using global::ProductStoreMVC.Models;
using global::ProductStoreMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ProductStoreMVC.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    // REGISTER (GET)
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // REGISTER (POST)
    [HttpPost]
    public async Task<IActionResult> Register(User model)
    {
        var result = await _userService.RegisterAsync(model);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError("", result.ErrorMessage);
            return View(model);
        }

        return RedirectToAction("Login");
    }

    // LOGIN (GET)
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // LOGIN (POST)
    [HttpPost]
    public async Task<IActionResult> Login(string userName, string password)
    {
        var result = await _userService.LoginAsync(userName, password);

        if (!result.IsSuccess)
        {
            ModelState.AddModelError("", result.ErrorMessage);
            return View();
        }

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return RedirectToAction("Index", "Home");
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Login");
    }
}
