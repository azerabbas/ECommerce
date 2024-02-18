using ECommerceApi.Application.Abstraction.Services;
using ECommerceApi.Application.Abstraction.Token;
using ECommerceApi.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Practices.EnterpriseLibrary.Validation.Configuration;
using A = ECommerceApi.Domain.Entities.Identity;


namespace ECommerceApi.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly IAuthService _aAuthService;

        public GoogleLoginCommandHandler(IAuthService aAuthService)
        {
            _aAuthService=aAuthService;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {

            var token = await _aAuthService.GoogleLoginAsync(request.IdToken, 900);
            return new()
            {
                Token = token
            };

        }
    }
}
