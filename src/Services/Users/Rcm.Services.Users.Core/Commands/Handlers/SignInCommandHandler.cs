using Genl.Auth.JWT;
using Genl.Framework.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rcm.Services.Users.Core.DAL;
using Rcm.Services.Users.Core.Entities;

namespace Rcm.Services.Users.Core.Commands.Handlers;
public class SignInCommandHandler : IRequestHandler<SignInCommand, JsonWebToken>
{
    private readonly UsersDbContext _dbContext;
    private readonly IJwtHandler _jwtHandler;
    private readonly IPasswordHasher<object> _passwordHasher;

    public SignInCommandHandler(UsersDbContext dbContext, IJwtHandler jwtHandler, IPasswordHasher<object> passwordHasher)
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
        if (user == null || _passwordHasher.VerifyHashedPassword(new object(), user.Password, request.Password) == PasswordVerificationResult.Failed)
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
