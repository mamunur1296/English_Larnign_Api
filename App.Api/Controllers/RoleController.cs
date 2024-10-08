using App.Application.Features.RoleFeatures.CommandHandlers;
using App.Application.Features.RoleFeatures.QueryHandlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleCommand commend)
        {
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllRoleQuery());
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(string id)
        //{
        //    var result = await _mediator.Send(new GetByIdEmployeeQuery(id));
        //    return StatusCode((int)HttpStatusCode.OK, result);
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var result = await _mediator.Send(new DeleteEmployeeCommand(id));
        //    return StatusCode((int)HttpStatusCode.OK, result);
        //}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(string id, UpdateEmployeeCommand commend)
        //{
        //    commend.id = id;
        //    var result = await _mediator.Send(commend);
        //    return StatusCode((int)HttpStatusCode.Created, result);
        //}
    }
}
