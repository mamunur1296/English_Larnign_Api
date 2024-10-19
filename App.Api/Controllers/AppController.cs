using App.Application.AppFeatures.QueryHandlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("AllCatagorys")]
        public async Task<IActionResult> AllCategorys()
        {
            var result = await _mediator.Send(new GetAllCategoryQuery());
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpGet("AllSubCategorys")]
        public async Task<IActionResult> AllSubCategorys()
        {
            var result = await _mediator.Send(new GetAllAppSubCategoryQuery());
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpGet("AllSentenceForms")]
        public async Task<IActionResult> AllSentenceForms()
        {
            var result = await _mediator.Send(new GetAllAppSentenceFormsQuery());
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpGet("AllSentence")]
        public async Task<IActionResult> AllSentence(string subCatagoryId, string formId, int? pageSize = null, int? pageNumber = null)
        {
            // Create and send the query with the parameters
            var result = await _mediator.Send(new GetAllSentenceStructureAppQuery(subCatagoryId, formId, pageSize, pageNumber));

            return StatusCode((int)HttpStatusCode.OK, result);
        }


    }
}
