using MediatR;

namespace Rcm.Services.Users.Core.CQRS;

public record SignUpCommand(string Email, string Password) : IRequest;