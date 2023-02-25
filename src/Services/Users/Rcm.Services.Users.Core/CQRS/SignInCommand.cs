using MediatR;
using Genl.Auth.JWT;

namespace Rcm.Services.Users.Core.CQRS;

public record SignInCommand(string Email, string Password) : IRequest<JsonWebToken>;