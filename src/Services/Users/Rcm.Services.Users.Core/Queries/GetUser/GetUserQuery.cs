using System;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using Rcm.Services.Users.Core.DAL;

namespace Rcm.Services.Users.Core.Queries.GetUser;

public class GetUserQuery : IRequest<int>
{
    public int UserId { get; set; }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, int>
{
    private readonly UsersDbContext _context;

    public GetUserQueryHandler(UsersDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(235);
    }
}