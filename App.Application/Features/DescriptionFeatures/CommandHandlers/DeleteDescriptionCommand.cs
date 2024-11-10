using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;

using System.Net;


namespace App.Application.Features.DescriptionFeatures.CommandHandlers
{
    public class DeleteDescriptionCommand : IRequest<Response<string>>
    {
        public DeleteDescriptionCommand(string id)
        {
            this.id = id;
        }

        public string id { get; private set; }
    }
    public class DeleteDescriptionHandler : IRequestHandler<DeleteDescriptionCommand, Response<string>>
    {

        private readonly IDescriptionServices _services;


        public DeleteDescriptionHandler(IDescriptionServices services)
        {
            _services = services;
        }

        public async Task<Response<string>> Handle(DeleteDescriptionCommand request, CancellationToken cancellationToken)
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
                    Message = "Description Delete successfully."
                };
            }
            throw new BadRequestException("Failed to Delete the Description. Please try again.");
        }
    }
}
