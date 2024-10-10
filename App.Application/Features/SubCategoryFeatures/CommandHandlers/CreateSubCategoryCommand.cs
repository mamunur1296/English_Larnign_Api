using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;

using System.Net;


namespace App.Application.Features.SubCategoryFeatures.CommandHandlers
{
    public class CreateSubCategoryCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class CreateSubCategoryHandler : IRequestHandler<CreateSubCategoryCommand, Response<string>>
    {

        private readonly ISubCategoryServices _services;


        public CreateSubCategoryHandler(ISubCategoryServices services)
        {
            _services = services;
        }
        public async Task<Response<string>> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
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
                    Message = "Sub Category created successfully."
                };
            }
            throw new BadRequestException("Failed to create the Sub Category. Please try again.");
        }
    }
}
