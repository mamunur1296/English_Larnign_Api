using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.CategoryFeatures.CommandHandlers
{
    public class UpdateCategoryCommand : IRequest<Response<string>>
    {
        public string? id { get; set; }
        public string Name { get; set; }
        public string? UpdatedBy { get; set; }

    }

    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, Response<string>>
    {
        private readonly ICategoryServices _services;

        public UpdateCategoryHandler(ICategoryServices services)
        {
            _services = services;
            
        }

        public async Task<Response<string>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
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
                    Message = "Category Update successfully."
                };
            }
            throw new BadRequestException("Failed to Update the Category. Please try again.");
        }
    }
}
