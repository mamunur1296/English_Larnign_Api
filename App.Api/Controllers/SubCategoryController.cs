
using App.Application.Features.SentenceFormsFeatures.CommandHandlers;
using App.Application.Features.SubCategoryFeatures.CommandHandlers;
using App.Application.Features.SubCategoryFeatures.QueryHandlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSubCategoryCommand commend)
        {
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
        [HttpPost("AssainForms")]
        public async Task<IActionResult> AssainForms(CreateAssainFormCommand commend)
        {
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllSubCategoryQuery());
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetByIdSubCategoryQuery(id));
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpGet("SearchSubCategory")]
        public async Task<IActionResult> SearchBySubCategory(string dubCatagoryid, string VerbId)
        {
            var result = await _mediator.Send(new SearchBySubCategoryQuery(dubCatagoryid, VerbId));
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteSubCategoryCommand(id));
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateSubCategoryCommand commend)
        {
            commend.id = id;
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
    }
}
