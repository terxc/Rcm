using MediatR;
using Rcm.Shared.Auth;

namespace Rcm.Services.Users.Core.Commands;

public record SignInCommand(string Email, string Password) : IRequest<JsonWebToken>;