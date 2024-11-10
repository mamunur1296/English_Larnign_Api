using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;


namespace App.Application.Features.DescriptionFeatures.CommandHandlers
{
    public class UpdateDescriptionCommand : IRequest<Response<string>>
    {
        public string? id { get; set; }
        public string body { get; set; }
        public string formateId { get; set; }
        public string subCatagoryId { get; set; }

    }

    public class UpdateDescriptionHandler : IRequestHandler<UpdateDescriptionCommand, Response<string>>
    {
        private readonly IDescriptionServices _services;

        public UpdateDescriptionHandler(IDescriptionServices services)
        {
            _services = services;
        }

        public async Task<Response<string>> Handle(UpdateDescriptionCommand request, CancellationToken cancellationToken)
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
                    Message = "Description Update successfully."
                };
            }
            throw new BadRequestException("Failed to Update the Description. Please try again.");
        }
    }
}
