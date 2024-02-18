using ECommerceApi.Application.Features.Commands.AppUser.FacebookLogin;
using ECommerceApi.Application.Features.Commands.AppUser.GoogleLogin;
using ECommerceApi.Application.Features.Commands.AppUser.LoginUser;
using ECommerceApi.Application.Features.Commands.AppUser.RefreshToken;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator=mediator;
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {
            LoginUserCommandResponse result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromForm] RefreshTokenCommandRequest request)
        {
            RefreshTokenCommandResponse response = await _mediator.Send(request);
            return Ok(response);

        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest request)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpPost("facebook-login")]
        public async Task<IActionResult> GoogleLogin(FacebookLoginCommandRequest request)
        {
            FacebookLoginCommandResponse response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
