using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rcm.Services.Users.Core.DAL;

namespace Rcm.Services.Users.Core.Commands.SignUp;

public class SignUpCommand : IRequest<int>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
