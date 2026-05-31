using Microsoft.EntityFrameworkCore;
using ProductStoreMVC.Data;
using ProductStoreMVC.Repositories;
using ProductStoreMVC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// 1. MVC
// Ավելացնում ենք MVC-ի աջակցությունը ծրագրին։
// Սա հնարավորություն է տալիս օգտագործել Controllers, Views և Routing։
builder.Services.AddControllersWithViews();

// 2. Database
// Գրանցում ենք AppDbContext-ը Dependency Injection համակարգում։
//
// AppDbContext-ը EF Core-ի հիմնական class-ն է,
// որի միջոցով մեր ծրագիրը կապվում է SQL Server-ի հետ։
//
// Connection String-ը վերցվում է appsettings.json ֆայլից։
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

// 3. Services
// Եթե Controller-ը կամ Service-ը IUserService պահանջի,
// Dependency Injection-ը կտրամադրի UserService instance։
builder.Services.AddScoped<IUserService, UserService>();

// 4. Repositories
// Եթե ծրագրի որևէ հատված IUserRepository պահանջի,
// ASP.NET Core-ը ավտոմատ կստեղծի և կտա UserRepository օբյեկտ։
builder.Services.AddScoped<IUserRepository, UserRepository>();

// 5. Authentication
// Միացնում ենք Authentication համակարգը։
//
// Authentication = "Ո՞վ է օգտատերը"
//
// Մենք ընտրում ենք Cookie Authentication,
// այսինքն Login-ից հետո browser-ում cookie է պահպանվելու։
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    // Կարգավորում ենք Cookie Authentication-ը։
    .AddCookie(options =>
    {
        // Եթե user-ը login արած չէ,
        // բայց փորձում է մտնել [Authorize] էջ,
        // ավտոմատ կուղղորդվի Login էջ։
        options.LoginPath = "/Account/Login";

        // Եթե user-ը login արած է,
        // բայց չունի բավարար իրավունքներ,
        // կուղղորդվի AccessDenied էջ։
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// 6. Build
// Կառուցում ենք Web Application-ը։
//
// Այս պահից հետո Services-ի գրանցումը ավարտվում է,
// և սկսում ենք կարգավորել Request Pipeline-ը։
var app = builder.Build();

// 7. Production Error Handling
// Եթե ծրագիրը աշխատում է Production միջավայրում
// (ոչ թե Development),
// օգտագործում ենք հատուկ Error էջ։
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // Browser-ին ասում ենք միշտ HTTPS օգտագործել։
    app.UseHsts();
}

// 8. HTTPS Redirect
// Եթե request-ը HTTP-ով է եկել,
// ավտոմատ տեղափոխում ենք HTTPS տարբերակ։
app.UseHttpsRedirection();

// 9. Static Files
// Թույլ ենք տալիս browser-ին ստանալ
// CSS, JavaScript, Images և այլ Static Files։
app.MapStaticAssets();

// 10. Routing
// Routing-ը հասկանում է,
// թե URL-ը որ Controller-ի և Action-ի հետ է կապված։
app.UseRouting();

// 11. Authentication
// Ստուգում է՝ browser-ը authentication cookie ուղարկել է, թե ոչ։
//
// Եթե cookie կա,
// ստեղծվում է HttpContext.User օբյեկտը,
// և ծրագիրը հասկանում է՝ ով է մուտք գործած օգտատերը։
app.UseAuthentication();

// 12. Authorization
// Ստուգում է [Authorize] attribute-ները։
//
// Եթե user-ը չունի մուտքի իրավունք,
// Controller Action-ին չի հասնի։
app.UseAuthorization();

// 13. Routes
// Default Route.
//
// Եթե URL-ում controller/action նշված չէ,
// կբացվի HomeController-ի Index action-ը։
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// 14. Run
// Ծրագիրը սկսում է աշխատել
// և սպասում է browser-ից եկող request-ներին։
app.Run();
