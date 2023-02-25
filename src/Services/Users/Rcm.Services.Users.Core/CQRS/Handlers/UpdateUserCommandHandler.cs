using MediatR;
using Rcm.Services.Users.Core.DAL;

namespace Rcm.Services.Users.Core.CQRS.Handlers;
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly UsersDbContext _dbContext;

    public UpdateUserCommandHandler(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        return Unit.Value;
    }
}
