using MediatR;

namespace Rcm.Services.Users.Core.CQRS;

public record UpdateUserCommand(int Id) : IRequest;