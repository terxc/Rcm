namespace Rcm.Services.Users.Core.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<Permission> Permissions { get; set; } = new List<Permission>();
    public IEnumerable<User> Users { get; set; } = new List<User>();

    public const string Admin = "admin";
    public const string Manager = "manager";
    public const string User = "user";
    public static string Default => User;
}
