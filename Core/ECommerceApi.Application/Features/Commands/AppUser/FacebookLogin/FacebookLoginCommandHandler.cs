using ECommerceApi.Application.Abstraction.Services;
using ECommerceApi.Application.Abstraction.Token;
using ECommerceApi.Application.DTOs;
using ECommerceApi.Application.DTOs.Facebook;
using ECommerceApi.Application.DTOs.Facebook.FacebookUserAccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using A = ECommerceApi.Domain.Entities.Identity;

namespace ECommerceApi.Application.Features.Commands.AppUser.FacebookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public FacebookLoginCommandHandler(IAuthService authService)
        {
            _authService=authService;
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
        {

            var token = await _authService.FacebookLoginAsync(request.AuthToken,900);

            return new()
            {
                Token = token
            };


        }
    }
}
