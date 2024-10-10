using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;

using System.Net;

namespace App.Application.Features.SentenceFormsFeatures.CommandHandlers
{
    public class UpdateSentenceFormsCommand : IRequest<Response<string>>
    {
        public string? id { get; set; }
        public string Name { get; set; }
        public string? UpdatedBy { get; set; }

    }

    public class UpdateSentenceFormsHandler : IRequestHandler<UpdateSentenceFormsCommand, Response<string>>
    {
        private readonly ISentenceFormsServices _services;

        public UpdateSentenceFormsHandler(ISentenceFormsServices services)
        {
            _services = services;

        }

        public async Task<Response<string>> Handle(UpdateSentenceFormsCommand request, CancellationToken cancellationToken)
        {

            var result = await _services.UpdateAsync(request);

            if (result.Success)
            {
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    id = result.id,
                    Data = "Success",
                    Message = "Sentence Forms Update successfully."
                };
            }
            throw new BadRequestException("Failed to Update the Sentence Forms. Please try again.");
        }
    }
}
