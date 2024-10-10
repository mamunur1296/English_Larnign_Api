using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;


namespace App.Application.Features.SentenceStructureFeatures.CommandHandlers
{
    public class CreateSentenceStructureCommand : IRequest<Response<string>>
    {
        public string BanglaSentence { get; set; }
        public string EnglistSentence { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class CreateSentenceStructureHandler : IRequestHandler<CreateSentenceStructureCommand, Response<string>>
    {

        private readonly ISentenceStructureServices _services;


        public CreateSentenceStructureHandler(ISentenceStructureServices services)
        {
            _services = services;
        }
        public async Task<Response<string>> Handle(CreateSentenceStructureCommand request, CancellationToken cancellationToken)
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
                    Message = "Sentence Structure  created successfully."
                };
            }
            throw new BadRequestException("Failed to create the Sentence Structure . Please try again.");
        }
    }
}
