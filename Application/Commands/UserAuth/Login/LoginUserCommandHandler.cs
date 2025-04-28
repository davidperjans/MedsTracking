using Domain.Interfaces;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UserAuth.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, OperationResult<string>>
    {
        private readonly IUserService _userService;

        public LoginUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<OperationResult<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.LoginUserAsync(request.Email, request.Password);
            if (!result)
                return OperationResult<string>.Failure("Error Logging in");

            return OperationResult<string>.Success("User Logged in");
        }
    }
}
