using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;


namespace App.Application.Features.SubCategoryFeatures.CommandHandlers
{
    public class DeleteSubCategoryCommand : IRequest<Response<string>>
    {
        public DeleteSubCategoryCommand(string id)
        {
            this.id = id;
        }

        public string id { get; private set; }
    }
    public class DeleteSubCategoryHandler : IRequestHandler<DeleteSubCategoryCommand, Response<string>>
    {

        private readonly ISubCategoryServices _services;


        public DeleteSubCategoryHandler(ISubCategoryServices services)
        {
            _services = services;
        }

        public async Task<Response<string>> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
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
                    Message = "Sub Category Delete successfully."
                };
            }
            throw new BadRequestException("Failed to Delete the Sub Category. Please try again.");
        }
    }
}
