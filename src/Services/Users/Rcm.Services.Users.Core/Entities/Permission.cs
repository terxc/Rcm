using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rcm.Services.Users.Core.Entities;
public class Permission
{
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Role> Roles { get; set; }

    public const string UsersView = "users:view";
    public const string UsersEdit = "users:edit";
}
