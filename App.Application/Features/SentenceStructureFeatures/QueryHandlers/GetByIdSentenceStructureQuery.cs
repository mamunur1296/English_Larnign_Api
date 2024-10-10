using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.SentenceStructureFeatures.QueryHandlers
{
    public class GetByIdSentenceStructureQuery : IRequest<Response<SentenceStructureDTOs>>
    {
        public GetByIdSentenceStructureQuery(string id)
        {
            this.id = id;
        }

        public string id { get; private set; }
    }

    public class GetByIdSentenceStructureHandler : IRequestHandler<GetByIdSentenceStructureQuery, Response<SentenceStructureDTOs>>
    {

        private readonly ISentenceStructureServices _services;

        public GetByIdSentenceStructureHandler(ISentenceStructureServices services)
        {
            _services = services;
        }
        public async Task<Response<SentenceStructureDTOs>> Handle(GetByIdSentenceStructureQuery request, CancellationToken cancellationToken)
        {
            var result = await _services.GetByIdAsync(request.id);
            if (result != null)
            {
                return new Response<SentenceStructureDTOs>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = " Get Sentence Structure successfully."
                };
            }
            throw new BadRequestException("Failed to create the Sentence Structure . Please try again.");
        }
    }
}
