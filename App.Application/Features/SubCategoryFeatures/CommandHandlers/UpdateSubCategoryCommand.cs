using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;


namespace App.Application.Features.SubCategoryFeatures.CommandHandlers
{
    public class UpdateSubCategoryCommand : IRequest<Response<string>>
    {
        public string? id { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string? UpdatedBy { get; set; }

    }

    public class UpdateSubCategoryHandler : IRequestHandler<UpdateSubCategoryCommand, Response<string>>
    {
        private readonly ISubCategoryServices _services;

        public UpdateSubCategoryHandler(ISubCategoryServices services)
        {
            _services = services;

        }

        public async Task<Response<string>> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
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
                    Message = "Sub Category Update successfully."
                };
            }
            throw new BadRequestException("Failed to Update the Sub Category. Please try again.");
        }
    }
}
