using App.Application.Features.AuthFeatures.CommandHandlers;
using App.Application.Features.EmployeeFeatures.CommandHandlers;
using App.Application.Features.UserFeatures.CommandHandlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("user_create")]
        public async Task<IActionResult> Create(RegisterUserCommend commend)
        {
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
        [HttpPost("user_login")]
        public async Task<IActionResult> Login( UserLoginCommend commend)
        {
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword( ChangePasswordCommend commend)
        {
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }

    }
}
