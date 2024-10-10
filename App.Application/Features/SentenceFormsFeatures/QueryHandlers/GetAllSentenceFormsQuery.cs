using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.SentenceFormsFeatures.QueryHandlers
{
    public class GetAllSentenceFormsQuery : IRequest<Response<IEnumerable<SentenceFormsDTOs>>>
    {
    }

    public class GetAllSentenceFormsHandler : IRequestHandler<GetAllSentenceFormsQuery, Response<IEnumerable<SentenceFormsDTOs>>>
    {
        private readonly ISentenceFormsServices _services;

        public GetAllSentenceFormsHandler(ISentenceFormsServices services)
        {
            _services = services;
        }
        public async Task<Response<IEnumerable<SentenceFormsDTOs>>> Handle(GetAllSentenceFormsQuery request, CancellationToken cancellationToken)
        {

            var result = await _services.GetAllAsync();
            if (result != null)
            {
                return new Response<IEnumerable<SentenceFormsDTOs>>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = "Sentence Forms created successfully."
                };
            }
            throw new BadRequestException("Failed to create the Sentence Forms. Please try again.");

        }
    }
}
