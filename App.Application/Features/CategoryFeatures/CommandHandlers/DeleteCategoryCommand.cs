using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.CategoryFeatures.CommandHandlers
{
    public class DeleteCategoryCommand : IRequest<Response<string>>
    {
        public DeleteCategoryCommand(string id)
        {
            this.id = id;
        }

        public string id { get; private set; }
    }
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Response<string>>
    {

        private readonly ICategoryServices _services;


        public DeleteCategoryHandler(ICategoryServices services)
        {
            _services = services;
        }

        public async Task<Response<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
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
                    Message = "Category Delete successfully."
                };
            }
            throw new BadRequestException("Failed to Delete the Category. Please try again.");
        }
    }
}
