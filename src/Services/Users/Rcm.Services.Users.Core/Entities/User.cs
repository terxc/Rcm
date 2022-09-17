using System;
using System.Collections.Generic;

namespace Rcm.Services.Users.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public IEnumerable<Role> Roles { get; set; }
    public UserState State { get; set; }
    public DateTime CreatedDate { get; set; }
    private string TestField { get; set; }
}

public enum UserState
{
    Active = 1,
    Locked = 2
}
