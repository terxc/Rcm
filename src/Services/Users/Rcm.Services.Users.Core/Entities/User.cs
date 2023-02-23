namespace Rcm.Services.Users.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public IEnumerable<Role> Roles { get; set; } = new List<Role>();
    public UserState State { get; set; }
    public DateTime CreatedDate { get; set; }
}

public enum UserState
{
    Active = 1,
    Locked = 2
}
