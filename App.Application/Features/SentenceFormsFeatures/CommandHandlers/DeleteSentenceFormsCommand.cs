using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.SentenceFormsFeatures.CommandHandlers
{
    public class DeleteSentenceFormsCommand : IRequest<Response<string>>
    {
        public DeleteSentenceFormsCommand(string id)
        {
            this.id = id;
        }

        public string id { get; private set; }
    }
    public class DeleteSentenceFormsHandler : IRequestHandler<DeleteSentenceFormsCommand, Response<string>>
    {

        private readonly ISentenceFormsServices _services;


        public DeleteSentenceFormsHandler(ISentenceFormsServices services)
        {
            _services = services;
        }

        public async Task<Response<string>> Handle(DeleteSentenceFormsCommand request, CancellationToken cancellationToken)
        {
            var result = await _services.DeleteAsync(request.id);
            if (result.Success)
            {
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.OK, // You can set the status code here
                    id = result.id,
                    Data = "Success",
                    Message = "Sentence Forms Delete successfully."
                };
            }
            throw new BadRequestException("Failed to Delete the Sentence Forms. Please try again.");
        }
    }
}
