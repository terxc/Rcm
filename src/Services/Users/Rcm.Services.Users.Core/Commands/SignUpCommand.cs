using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rcm.Services.Users.Core.DAL;

namespace Rcm.Services.Users.Core.Commands;

public class SignUpCommand : IRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
