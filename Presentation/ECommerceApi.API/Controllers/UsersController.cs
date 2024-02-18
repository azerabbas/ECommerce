using ECommerceApi.Application.Features.Commands.AppUser;
using ECommerceApi.Application.Features.Commands.AppUser.FacebookLogin;
using ECommerceApi.Application.Features.Commands.AppUser.GoogleLogin;
using ECommerceApi.Application.Features.Commands.AppUser.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator=mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest request)
        {
          CreateUserCommandResponse result =   await _mediator.Send(request);
            return Ok(result);
        }


    }
}
