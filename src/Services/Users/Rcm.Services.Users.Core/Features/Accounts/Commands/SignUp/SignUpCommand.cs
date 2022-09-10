using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rcm.Services.Users.Core.DAL;

namespace Rcm.Services.Users.Core.Features.Accounts.Commands.SignUp;

public class SignUpCommand : IRequest<int>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

internal class SignUpCommandHandler : IRequestHandler<SignUpCommand, int>
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
