using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.CategoryFeatures.CommandHandlers
{
    public class CreateCategoryCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Response<string>>
    {

        private readonly ICategoryServices _services;


        public CreateCategoryHandler(ICategoryServices services)
        {
            _services = services;
        }
        public async Task<Response<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
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
                    Message = "Category created successfully."
                };
            }
            throw new BadRequestException("Failed to create the Category. Please try again.");
        }
    }
}
