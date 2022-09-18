using System.Collections.Generic;

namespace Rcm.Services.Users.Core.Entities;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Permission> Permissions { get; set; }
    public IEnumerable<User> Users { get; set; }

    public const string Admin = "admin";
    public const string Manager = "manager";
    public const string User = "user";
    public static string Default => User;
}
