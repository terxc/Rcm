using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rcm.Services.Users.Core.Features.Accounts.Commands.SignUp;

namespace Rcm.Services.Users.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly ISender _mediator;

    public AccountController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("sign-up")]
    public async Task<ActionResult<int>> SignUpAsync(SignUpCommand command)
    {
        return await _mediator.Send(command);
    }
}
