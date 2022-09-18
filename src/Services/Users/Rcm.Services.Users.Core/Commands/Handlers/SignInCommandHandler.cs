using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rcm.Services.Users.Core.DAL;
using Rcm.Services.Users.Core.Entities;
using Rcm.Shared.Auth;
using Rcm.Shared.Exceptions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Rcm.Services.Users.Core.Commands.Handlers;
public class SignInCommandHandler : IRequestHandler<SignInCommand, JsonWebToken>
{
    private readonly UsersDbContext _dbContext;
    private readonly IJwtHandler _jwtHandler;
    private readonly IPasswordHasher<User> _passwordHasher;

    public SignInCommandHandler(UsersDbContext dbContext, IJwtHandler jwtHandler, IPasswordHasher<User> passwordHasher)
    {
        _dbContext = dbContext;
        _jwtHandler = jwtHandler;
        _passwordHasher = passwordHasher;
    }

    public async Task<JsonWebToken> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(x => x.Roles).ThenInclude(x => x.Permissions)
            .FirstOrDefaultAsync(x => x.Email == request.Email);
        if (user == null || _passwordHasher.VerifyHashedPassword(default, user.Password, request.Password) == PasswordVerificationResult.Failed)
        {
            throw new CustomException("Неверный логин пользователя или пароль");
        }

        if (user.State != UserState.Active)
        {
            throw new CustomException("Пользователь заблокирован");
        }

        var roles = user.Roles.Select(x => x.Name);
        var claims = new Dictionary<string, IEnumerable<string>>
        {
            ["permissions"] = user.Roles.SelectMany(x => x.Permissions).Select(x => x.Name)
        };

        var jwt = _jwtHandler.CreateToken(user.Id.ToString(), roles, claims);
        return jwt;
    }
}
