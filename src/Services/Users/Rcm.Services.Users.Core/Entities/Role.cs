using System.Collections.Generic;

namespace Rcm.Services.Users.Core.Entities;

internal class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> Permissions { get; set; }
    public IEnumerable<UserRole> Users { get; set; }

    public static string Default => User;
    public const string User = "user";
    public const string Admin = "admin";
}
