using ECommerceApi.Application.Abstraction.Services;
using ECommerceApi.Application.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using U = ECommerceApi.Domain.Entities.Identity;

namespace ECommerceApi.Application.Features.Commands.AppUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService=userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponse response = await _userService.CreateAsync(new()
            {
                Username = request.Username,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                Email = request.Email,
                NameUsername = request.NameUsername
            });

            return new()
            {
                Message = response.Message, 
                Succeeded = response.Succeeded,
            };
            
        }
    }
}
