namespace ProductStoreMVC.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string PasswordHesh { get; set; }
    public string Role { get; set; } = "User";
}
