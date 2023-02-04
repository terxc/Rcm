using MediatR;

namespace Rcm.Services.Users.Core.Commands;

public record UpdateUserCommand(int Id) : IRequest;