using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;


namespace App.Application.Features.CategoryFeatures.QueryHandlers
{
    public class GetByIdCategoryQuery : IRequest<Response<CategoryDTOs>>
    {
        public GetByIdCategoryQuery(string id)
        {
            this.id = id;
        }

        public string id { get; private set; }
    }

    public class GetByIdCategoryHandler : IRequestHandler<GetByIdCategoryQuery, Response<CategoryDTOs>>
    {

        private readonly ICategoryServices _services;

        public GetByIdCategoryHandler(ICategoryServices services)
        {
            _services = services;
        }
        public async Task<Response<CategoryDTOs>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _services.GetByIdAsync(request.id);
            if (result != null)
            {
                return new Response<CategoryDTOs>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = " Get Category successfully."
                };
            }
            throw new BadRequestException("Failed to create the Category. Please try again.");
        }
    }
}
