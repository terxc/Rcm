using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Rcm.Services.Users.Core.Commands;
public class UpdateUserCommand : IRequest
{
    public int Id { get; set; }
}
