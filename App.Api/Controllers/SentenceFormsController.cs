using App.Application.Features.SentenceFormsFeatures.CommandHandlers;
using App.Application.Features.SentenceFormsFeatures.QueryHandlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentenceFormsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SentenceFormsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSentenceFormsCommand commend)
        {
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
        [HttpPost("AssainStructure")]
        public async Task<IActionResult> AssainStructure(CreateSentenceFormsAndStructureMapingCommand commend)
        {
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllSentenceFormsQuery());
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetByIdSentenceFormsQuery(id));
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteSentenceFormsCommand(id));
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateSentenceFormsCommand commend)
        {
            commend.id = id;
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
    }
}
