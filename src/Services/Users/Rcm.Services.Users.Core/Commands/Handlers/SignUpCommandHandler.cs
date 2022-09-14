using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rcm.Services.Users.Core.DAL;

namespace Rcm.Services.Users.Core.Commands.Handlers;
public class SignUpCommandHandler : IRequestHandler<SignUpCommand, int>
{
    private readonly UsersDbContext _context;

    public SignUpCommandHandler(UsersDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(123);
    }
}
