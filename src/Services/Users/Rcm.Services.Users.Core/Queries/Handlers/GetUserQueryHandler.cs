using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rcm.Services.Users.Core.DAL;

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
        return await Task.FromResult(235);
    }
}
