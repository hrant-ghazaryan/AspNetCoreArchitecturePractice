using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductStoreMVC.Migrations;

// Այս class-ը EF Core Migration է,
// որը նկարագրում է database-ի առաջին կառուցվածքը (Initial schema)
public partial class InitialCreate : Migration
{
    /// <summary>
    /// Up() մեթոդը կատարում է database-ի փոփոխությունները (CREATE)
    /// Այսինքն՝ այստեղ ստեղծվում են table-ները
    /// </summary>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Categories table-ի ստեղծում
        migrationBuilder.CreateTable(
            name: "Categories",
            columns: table => new
            {
                // Primary Key column
                // Identity նշանակում է՝ ավտոմատ աճող թիվ (1,2,3,...)
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                // Category անունը (չի կարող լինել null)
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                // Primary Key սահմանում
                table.PrimaryKey("PK_Categories", x => x.Id);
            });

        // Products table-ի ստեղծում
        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                // Primary Key
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),

                // Product անուն
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),

                // Գինը (decimal՝ precision 18,2)
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),

                // Foreign Key դեպի Categories table
                CategoryId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                // Primary Key սահմանում Products table-ի համար
                table.PrimaryKey("PK_Products", x => x.Id);

                // Foreign Key կապ Category-ի հետ
                // Եթե Category ջնջվում է → կապված Products-ն էլ կջնջվեն (Cascade)
                table.ForeignKey(
                    name: "FK_Products_Categories_CategoryId",
                    column: x => x.CategoryId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        // Index ստեղծում CategoryId-ի վրա
        // Սա արագացնում է որոնումները (JOIN / WHERE CategoryId = ?)
        migrationBuilder.CreateIndex(
            name: "IX_Products_CategoryId",
            table: "Products",
            column: "CategoryId");
    }

    /// <summary>
    /// Down() մեթոդը rollback-ի համար է
    /// Եթե migration-ը հետ բերես՝ սա է աշխատում
    /// </summary>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        // Նախ ջնջում ենք Products table-ը
        migrationBuilder.DropTable(
            name: "Products");

        // Հետո Categories table-ը
        migrationBuilder.DropTable(
            name: "Categories");
    }
}
