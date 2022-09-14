using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rcm.Services.Users.Core.DAL;
using Rcm.Shared.Auth;

namespace Rcm.Services.Users.Core.Commands.Handlers;
public class SignInCommandHandler : IRequestHandler<SignInCommand, JsonWebToken>
{
    private readonly UsersDbContext _context;
    private readonly IJwtHandler _jwtHandler;

    public SignInCommandHandler(UsersDbContext context, IJwtHandler jwtHandler)
    {
        _context = context;
        _jwtHandler = jwtHandler;
    }

    public async Task<JsonWebToken> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var jwt = _jwtHandler.CreateToken("1234", "admin", claims: null);
        return jwt;
    }
}
