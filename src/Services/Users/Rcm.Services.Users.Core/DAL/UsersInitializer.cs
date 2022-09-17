﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
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
        var permissions = new string[] { "administration", "users" };

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
            Name = "admin",
            Permissions = permissions
        });
        await _dbContext.Roles.AddAsync(new Role
        {
            Name = "user",
            Permissions = new List<Permission>()
        });

        await _dbContext.SaveChangesAsync();
    }
}
