using Microsoft.EntityFrameworkCore;
using ProductStoreMVC.Models;

namespace ProductStoreMVC.Data;

// AppDbContext-ը ներկայացնում է կապը ծրագրի և database-ի միջև։
// EF Core-ը այս class-ի միջոցով է աշխատում SQL Server-ի հետ։
public class AppDbContext : DbContext
{
    // Constructor Injection
    // ASP.NET Core-ի Dependency Injection համակարգը ավտոմատ փոխանցում է DbContext-ի կարգավորումները։
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { } // options-ը փոխանցվում է DbContext base class-ին։


    // Products աղյուսակի ներկայացումն է ծրագրում։
    // EF Core-ը Product entity-ն կապում է Products table-ի հետ։
    public DbSet<Product> Products { get; set; }


    // Categories աղյուսակի ներկայացումն է ծրագրում։
    // EF Core-ը Category entity-ն կապում է Categories table-ի հետ։
    public DbSet<Category> Categories { get; set; }
}
