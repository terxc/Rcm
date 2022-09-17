using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rcm.Services.Users.Core.DAL;
using Rcm.Services.Users.Core.Entities;

namespace Rcm.Services.Users.Core.Commands.Handlers;
public class SignUpCommandHandler : IRequestHandler<SignUpCommand>
{
    private readonly UsersDbContext _dbContext;
    private readonly IPasswordHasher<User> _passwordHasher;

    public SignUpCommandHandler(UsersDbContext dbContext, IPasswordHasher<User> passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<Unit> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var defaultRole = await _dbContext.Roles.FirstAsync(x => x.Name == Role.Default);

        var email = request.Email.ToLowerInvariant();
        var password = _passwordHasher.HashPassword(default, request.Password);
        var user = new User
        {
            Email = request.Email,
            Password = password,
            Roles = new List<Role> { defaultRole },
            CreatedDate = DateTime.Now,
            State = UserState.Active,
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();

        return Unit.Value;
    }
}
