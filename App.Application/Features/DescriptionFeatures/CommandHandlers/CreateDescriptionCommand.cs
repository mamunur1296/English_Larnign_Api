using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Features.EmployeeFeatures.CommandHandlers;
using App.Application.Interfaces;
using FluentValidation;
using MediatR;

using System.Net;


namespace App.Application.Features.DescriptionFeatures.CommandHandlers
{
    public class CreateDescriptionCommand : IRequest<Response<string>>
    {
        public string body { get; set; }
        public string formateId { get; set; }
        public string subCatagoryId { get; set; }

    }
    public class CreateDescriptionHandler : IRequestHandler<CreateDescriptionCommand, Response<string>>
    {
        private readonly IDescriptionServices _services;

        public CreateDescriptionHandler(IDescriptionServices services)
        {
            _services = services;
        }

        public async Task<Response<string>> Handle(CreateDescriptionCommand request, CancellationToken cancellationToken)
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
                    Message = "Description created successfully."
                };
            }
            throw new BadRequestException("Failed to create the Description. Please try again.");
        }
    }
}
