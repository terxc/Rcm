namespace Rcm.Services.Users.Core.Entities;
public class Permission
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<Role> Roles { get; set; } = new List<Role>();

    public const string UsersView = "users:view";
    public const string UsersEdit = "users:edit";
}
