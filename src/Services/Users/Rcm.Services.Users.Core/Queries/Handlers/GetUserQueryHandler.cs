using MediatR;
using Microsoft.EntityFrameworkCore;
using Rcm.Services.Users.Core.DAL;
using Rcm.Shared.Exceptions;

namespace Rcm.Services.Users.Core.Queries.Handlers;
public class GetUserQueryHandler : IRequestHandler<GetUserQuery, int>
{
    private readonly UsersDbContext _dbContext;

    public GetUserQueryHandler(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .Include(x => x.Roles).ThenInclude(x => x.Permissions)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (user == null) {
            throw new NotFoundException();
        }

        return await Task.FromResult(user.Id);
    }
}
