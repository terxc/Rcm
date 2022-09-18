using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rcm.Services.Users.Core.DAL;

namespace Rcm.Services.Users.Core.Commands.Handlers;
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
