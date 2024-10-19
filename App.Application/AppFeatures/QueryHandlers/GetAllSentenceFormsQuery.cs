using App.Application.AppFeatures.AppDtos;
using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.AppFeatures.QueryHandlers
{
    public class GetAllAppSentenceFormsQuery : IRequest<Response<IEnumerable<SentenceFormsAppDTOs>>>
    {
    }

    public class GetAllSentenceFormsHandler : IRequestHandler<GetAllAppSentenceFormsQuery, Response<IEnumerable<SentenceFormsAppDTOs>>>
    {
        private readonly ISentenceFormsServices _services;

        public GetAllSentenceFormsHandler(ISentenceFormsServices services)
        {
            _services = services;
        }
        public async Task<Response<IEnumerable<SentenceFormsAppDTOs>>> Handle(GetAllAppSentenceFormsQuery request, CancellationToken cancellationToken)
        {

            var result = await _services.GetAllAsync();
            var AppResult = result.Select(sf => new SentenceFormsAppDTOs
            {
                Id = sf.Id,
                Name = sf.Name,
                SubCategoryId = sf.SubCategoryId,
            });
            if (result != null)
            {
                return new Response<IEnumerable<SentenceFormsAppDTOs>>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = AppResult,
                    Message = "Sentence Forms Get All successfully."
                };
            }
            throw new BadRequestException("Failed to create the Sentence Forms. Please try again.");

        }
    }
}
