using ECommerceApi.Application.Abstraction.Services;
using ECommerceApi.Application.Abstraction.Token;
using ECommerceApi.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A = ECommerceApi.Domain.Entities.Identity;

namespace ECommerceApi.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService=authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {

            var token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password,900);
            return new LoginUserCommandResponse()
            {
                Token = token,
            };

        }
    }
}
