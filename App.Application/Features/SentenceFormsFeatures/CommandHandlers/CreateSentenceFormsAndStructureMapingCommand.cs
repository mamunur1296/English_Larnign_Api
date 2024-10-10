using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;


namespace App.Application.Features.SentenceFormsFeatures.CommandHandlers
{
    public class CreateSentenceFormsAndStructureMapingCommand : IRequest<Response<string>>
    {
        public string formateID { get; set; }
        public List<string> structureIDs { get; set; } 
    }
    public class CreateSentenceFormsAndStructureMapingHandler : IRequestHandler<CreateSentenceFormsAndStructureMapingCommand, Response<string>>
    {

        private readonly ISentenceFormsServices _services;


        public CreateSentenceFormsAndStructureMapingHandler(ISentenceFormsServices services)
        {
            _services = services;
        }
        public async Task<Response<string>> Handle(CreateSentenceFormsAndStructureMapingCommand request, CancellationToken cancellationToken)
        {


            var result = await _services.AssainStructure(request.formateID,request.structureIDs);

            if (result)
            {
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = "Success",
                    Message = "Assaind  successfully."
                };
            }
            throw new BadRequestException("Failed to create the Sentence Forms. Please try again.");
        }
    }
}
