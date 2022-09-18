using MediatR;

namespace Rcm.Services.Users.Core.Queries;

public class GetUserQuery : IRequest<int>
{
    public int Id { get; set; }
}