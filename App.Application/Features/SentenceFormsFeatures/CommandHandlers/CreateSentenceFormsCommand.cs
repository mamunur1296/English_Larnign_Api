using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;


namespace App.Application.Features.SentenceFormsFeatures.CommandHandlers
{
    public class CreateSentenceFormsCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class CreateSentenceFormsHandler : IRequestHandler<CreateSentenceFormsCommand, Response<string>>
    {

        private readonly ISentenceFormsServices _services;


        public CreateSentenceFormsHandler(ISentenceFormsServices services)
        {
            _services = services;
        }
        public async Task<Response<string>> Handle(CreateSentenceFormsCommand request, CancellationToken cancellationToken)
        {


            var result = await _services.CreateAsync(request);

            if (result.Success)
            {
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    id = result.id,
                    Data = "Success",
                    Message = "Sentence Forms created successfully."
                };
            }
            throw new BadRequestException("Failed to create the Sentence Forms. Please try again.");
        }
    }
}
