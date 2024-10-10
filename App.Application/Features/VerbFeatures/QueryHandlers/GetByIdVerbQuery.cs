using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.VerbFeatures.QueryHandlers
{
    public class GetByIdVerbQuery : IRequest<Response<VerbDTOs>>
    {
        public GetByIdVerbQuery(string id)
        {
            this.id = id;
        }

        public string id { get; private set; }
    }

    public class GetByIdVerbHandler : IRequestHandler<GetByIdVerbQuery, Response<VerbDTOs>>
    {

        private readonly IVerbServices _services;

        public GetByIdVerbHandler(IVerbServices services)
        {
            _services = services;
        }
        public async Task<Response<VerbDTOs>> Handle(GetByIdVerbQuery request, CancellationToken cancellationToken)
        {
            var result = await _services.GetByIdAsync(request.id);
            if (result != null)
            {
                return new Response<VerbDTOs>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = " Get Verb successfully."
                };
            }
            throw new BadRequestException("Failed to create the Verb. Please try again.");
        }
    }
}
