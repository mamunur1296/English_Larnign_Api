using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.SentenceStructureFeatures.QueryHandlers
{
    public class GetAllSentenceStructureQuery : IRequest<Response<IEnumerable<SentenceStructureDTOs>>>
    {
    }

    public class GetAllSentenceStructureHandler : IRequestHandler<GetAllSentenceStructureQuery, Response<IEnumerable<SentenceStructureDTOs>>>
    {
        private readonly ISentenceStructureServices _services;

        public GetAllSentenceStructureHandler(ISentenceStructureServices services)
        {
            _services = services;
        }
        public async Task<Response<IEnumerable<SentenceStructureDTOs>>> Handle(GetAllSentenceStructureQuery request, CancellationToken cancellationToken)
        {

            var result = await _services.GetAllAsync();
            if (result != null)
            {
                return new Response<IEnumerable<SentenceStructureDTOs>>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = "Sentence Structure  created successfully."
                };
            }
            throw new BadRequestException("Failed to create the Sentence Structure . Please try again.");

        }
    }
}
