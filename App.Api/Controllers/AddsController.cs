﻿using App.Application.Features.AddsFeatures.CommandHandlers;
using App.Application.Features.AddsFeatures.QueryHandlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AddsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAddsCommand commend)
        {
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllAddsQuery());
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _mediator.Send(new GetByIdAddsQuery(id));
            return StatusCode((int)HttpStatusCode.OK, result);
        }
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    var result = await _mediator.Send(new DeleteCategoryCommand(id));
        //    return StatusCode((int)HttpStatusCode.OK, result);
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, UpdateAddsCommand commend)
        {
            commend.id = id;
            var result = await _mediator.Send(commend);
            return StatusCode((int)HttpStatusCode.Created, result);
        }
    }
}
