using Microsoft.EntityFrameworkCore;
using Rcm.Services.Users.Core.Entities;

namespace Rcm.Services.Users.Core.DAL;
public class UsersInitializer
{
    private readonly UsersDbContext _dbContext;

    public UsersInitializer(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task InitAsync()
    {
        if (await _dbContext.Roles.AnyAsync())
        {
            return;
        }

        await AddPermissionsAsync();
        await AddRolesAsync();
    }


    private async Task AddPermissionsAsync()
    {
        var permissions = new string[] { Permission.UsersView, Permission.UsersEdit };

        foreach (var p in permissions)
        {
            await _dbContext.Permissions.AddAsync(new Permission
            {
                Name = p
            });
        }

        await _dbContext.SaveChangesAsync();
    }

    private async Task AddRolesAsync()
    {
        var permissions = _dbContext.Permissions.ToList();
        await _dbContext.Roles.AddAsync(new Role
        {
            Name = Role.Admin,
            Permissions = permissions.Where(x => x.Name == Permission.UsersEdit).ToList()
        });
        await _dbContext.Roles.AddAsync(new Role
        {
            Name = Role.Manager,
            Permissions = permissions.Where(x => x.Name == Permission.UsersView).ToList()
        });
        await _dbContext.Roles.AddAsync(new Role
        {
            Name = Role.User
        });

        await _dbContext.SaveChangesAsync();
    }
}
