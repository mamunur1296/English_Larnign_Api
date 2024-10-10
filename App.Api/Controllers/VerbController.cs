using App.Application.Features.EmployeeFeatures.CommandHandlers;
using App.Application.Features.EmployeeFeatures.QueryHandlers;
using App.Application.Features.VerbFeatures.CommandHandlers;
using App.Application.Features.VerbFeatures.QueryHandlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerbController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VerbController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateVerbCommand commend)
        {
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllVerbQuery());
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetByIdVerbQuery(id));
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteVerbCommand(id));
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateVerbCommand commend)
        {
            commend.id = id;
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
    }
}
