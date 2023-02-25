using MediatR;

namespace Rcm.Services.Users.Core.CQRS;

public class GetUserQuery : IRequest<int>
{
    public int Id { get; set; }
}