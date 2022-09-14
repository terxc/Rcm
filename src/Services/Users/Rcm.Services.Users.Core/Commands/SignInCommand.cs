using MediatR;
using Rcm.Shared.Auth;

namespace Rcm.Services.Users.Core.Commands;
public class SignInCommand : IRequest<JsonWebToken>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
