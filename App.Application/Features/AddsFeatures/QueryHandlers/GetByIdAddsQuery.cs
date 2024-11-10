using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.AddsFeatures.QueryHandlers
{
    public class GetByIdAddsQuery : IRequest<Response<AddsDTO>>
    {
        public GetByIdAddsQuery(string id)
        {
            this.id = id;
        }

        public string id { get; private set; }
    }

    public class GetByIdAddsHandler : IRequestHandler<GetByIdAddsQuery, Response<AddsDTO>>
    {

        private readonly IAddsServices _services;

        public GetByIdAddsHandler(IAddsServices services)
        {
            _services = services;
        }
        public async Task<Response<AddsDTO>> Handle(GetByIdAddsQuery request, CancellationToken cancellationToken)
        {
            var result = await _services.GetByIdAsync(request.id);
            if (result != null)
            {
                return new Response<AddsDTO>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = " Adds Category successfully."
                };
            }
            throw new BadRequestException("Failed to create the Category. Please try again.");
        }
    }
}
