using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserAuth.Register
{
    public class RegisterUserCommand : IRequest<OperationResult<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
