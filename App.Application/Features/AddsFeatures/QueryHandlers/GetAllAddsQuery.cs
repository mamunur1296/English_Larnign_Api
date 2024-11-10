using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.AddsFeatures.QueryHandlers
{
    public class GetAllAddsQuery : IRequest<Response<IEnumerable<AddsDTO>>>
    {
    }

    public class GetAllAddsHandler : IRequestHandler<GetAllAddsQuery, Response<IEnumerable<AddsDTO>>>
    {
        private readonly IAddsServices _services;

        public GetAllAddsHandler(IAddsServices services)
        {
            _services = services;
        }
        public async Task<Response<IEnumerable<AddsDTO>>> Handle(GetAllAddsQuery request, CancellationToken cancellationToken)
        {

            var result = await _services.GetAllAsync();
            if (result != null)
            {
                return new Response<IEnumerable<AddsDTO>>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = "Adds created successfully."
                };
            }
            throw new BadRequestException("Failed to create the Adds. Please try again.");

        }
    }
}
