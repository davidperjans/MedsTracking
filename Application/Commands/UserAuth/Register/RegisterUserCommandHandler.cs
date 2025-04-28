using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserAuth.Register
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, OperationResult<string>>
    {
        private readonly IUserService _userService;

        public RegisterUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<OperationResult<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new User
            {
                Email = request.Email,
            };

            var result = _userService.RegisterUserAsync(newUser, request.Password);

            return OperationResult<string>.Success("User registered successfully");
        }
    }
}
