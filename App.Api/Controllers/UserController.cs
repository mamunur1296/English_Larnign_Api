using App.Application.Features.EmployeeFeatures.CommandHandlers;
using App.Application.Features.EmployeeFeatures.QueryHandlers;
using App.Application.Features.UserFeatures.CommandHandlers;
using App.Application.Features.UserFeatures.QueryHandlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("all_user")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpPut("update_user/{id}")]
        public async Task<IActionResult> Update(string id, UpdateUserCommand commend)
        {
            commend.id = id;
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
        [HttpDelete("delete_user/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteUserCommend(id));
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetByIdUserQuery(id));
            return StatusCode((int)HttpStatusCode.OK, result);
        }
    }
}
