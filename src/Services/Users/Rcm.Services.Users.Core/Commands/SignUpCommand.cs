using MediatR;

namespace Rcm.Services.Users.Core.Commands;

public record SignUpCommand(string Email, string Password) : IRequest;