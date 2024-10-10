using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.VerbFeatures.QueryHandlers
{
    public class GetAllVerbQuery : IRequest<Response<IEnumerable<VerbDTOs>>>
    {
    }

    public class GetAllVerbHandler : IRequestHandler<GetAllVerbQuery, Response<IEnumerable<VerbDTOs>>>
    {
        private readonly IVerbServices _services;

        public GetAllVerbHandler(IVerbServices services)
        {
            _services = services;
        }
        public async Task<Response<IEnumerable<VerbDTOs>>> Handle(GetAllVerbQuery request, CancellationToken cancellationToken)
        {

            var result = await _services.GetAllAsync();
            if (result != null)
            {
                return new Response<IEnumerable<VerbDTOs>>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = "Verb created successfully."
                };
            }
            throw new BadRequestException("Failed to create the Verb. Please try again.");

        }
    }
}
