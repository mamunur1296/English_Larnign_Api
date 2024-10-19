using App.Application.Features.SentenceStructureFeatures.CommandHandlers;
using App.Application.Features.SentenceStructureFeatures.QueryHandlers;
using App.Infrastructure.SeedData;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentenceStructureController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SentenceStructureController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSentenceStructureCommand commend)
        {
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllSentenceStructureQuery());
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetByIdSentenceStructureQuery(id));
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteSentenceStructureCommand(id));
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateSentenceStructureCommand commend)
        {
            commend.id = id;
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
        [HttpPost("import-excel-Sentence")]
        public async Task<IActionResult> ImportExcelSentence([FromForm] IFormFile file, [FromForm] CreateSentenceStructureFromXlsxFileCommand commend)
        {
            commend.file = file; 

            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }


    }
}
