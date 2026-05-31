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
        => _userService = userService;

    public IActionResult AccessDenied()
    {
        return View();
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
        /// <summary>
        /// Ուղարկում ենք user-ի տվյալները Service layer-ին
        /// որպեսզի ստեղծվի նոր user (registration logic)
        /// </summary>
        var result = await _userService.RegisterAsync(model);

        /// <summary>
        /// Եթե registration-ը չի հաջողվել,
        /// վերադարձնում ենք նույն View-ը և ցույց ենք տալիս error-ը
        /// </summary>
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("", result.ErrorMessage);
            return View(model);
        }

        /// <summary>
        /// Registration հաջող լինելու դեպքում
        /// redirect ենք անում Login էջ,
        /// որպեսզի user-ը նորից login անի
        /// </summary>
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
        /// <summary>
        /// ՔԱՅԼ 1 — Ստուգում ենք user-ի տվյալները
        /// Service layer-ը ստուգում է՝ username/password ճիշտ են թե ոչ
        /// Վերադարձնում է Result (Success կամ Fail)
        /// </summary>
        var result = await _userService.LoginAsync(userName, password);

        /// <summary>
        /// Եթե login-ը սխալ է,
        /// ցույց ենք տալիս error message-ը View-ում
        /// </summary>
        if (!result.IsSuccess)
        {
            ModelState.AddModelError("", result.ErrorMessage);
            return View();
        }

        /// <summary>
        /// ՔԱՅԼ 2 — Վերցնում ենք user-ին DB-ից
        /// Սա պետք է, որպեսզի ստանանք Name և Role տվյալները
        /// </summary>
        var user = await _userService.GetByUserNameAsync(userName);

        /// <summary>
        /// ՔԱՅԼ 3 — Ստեղծում ենք Claims list
        /// Claims-ը user-ի identity տվյալներն են (Name, Role և այլն)
        /// </summary>
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Name),

        /// Role-ը օգտագործվում է [Authorize(Roles="Admin")] համար
        new Claim(ClaimTypes.Role, user.Role)
    };

        /// <summary>
        /// ՔԱՅԼ 4 — Ստեղծում ենք Identity
        /// Identity-ն կապում է Claims-ը authentication համակարգի հետ
        /// </summary>
        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );

        /// <summary>
        /// ՔԱՅԼ 5 — Ստեղծում ենք Principal
        /// Principal-ը ASP.NET Core-ի կողմից օգտագործվող logged-in user-ն է
        /// </summary>
        var principal = new ClaimsPrincipal(identity);

        /// <summary>
        /// ՔԱՅԼ 6 — SIGN IN (ամենակարևոր քայլ)
        /// Այստեղ ստեղծվում է authentication cookie browser-ում
        ///
        /// Այս cookie-ն է ապացուցում, որ user-ը logged-in է
        /// </summary>
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal
        );

        /// <summary>
        /// ՔԱՅԼ 7 — Login հաջող է → redirect դեպի Home էջ
        /// </summary>
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Login");
    }
}
