using MediatR;
using Genl.Auth.JWT;

namespace Rcm.Services.Users.Core.Commands;

public record SignInCommand(string Email, string Password) : IRequest<JsonWebToken>;