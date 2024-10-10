using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.SentenceFormsFeatures.QueryHandlers
{
    public class GetByIdSentenceFormsQuery : IRequest<Response<SentenceFormsDTOs>>
    {
        public GetByIdSentenceFormsQuery(string id)
        {
            this.id = id;
        }

        public string id { get; private set; }
    }

    public class GetByIdSentenceFormsHandler : IRequestHandler<GetByIdSentenceFormsQuery, Response<SentenceFormsDTOs>>
    {

        private readonly ISentenceFormsServices _services;

        public GetByIdSentenceFormsHandler(ISentenceFormsServices services)
        {
            _services = services;
        }
        public async Task<Response<SentenceFormsDTOs>> Handle(GetByIdSentenceFormsQuery request, CancellationToken cancellationToken)
        {
            var result = await _services.GetByIdAsync(request.id);
            if (result != null)
            {
                return new Response<SentenceFormsDTOs>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = " Get Sentence Forms successfully."
                };
            }
            throw new BadRequestException("Failed to create the Sentence Forms. Please try again.");
        }
    }
}
