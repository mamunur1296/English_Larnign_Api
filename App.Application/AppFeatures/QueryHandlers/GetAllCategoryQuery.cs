using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.AppFeatures.QueryHandlers
{
    public class GetAllCategoryQuery : IRequest<Response<IEnumerable<CategoryDTOs>>>
    {
    }

    public class GetAllCategoryHandler : IRequestHandler<GetAllCategoryQuery, Response<IEnumerable<CategoryDTOs>>>
    {
        private readonly ICategoryServices _services;

        public GetAllCategoryHandler(ICategoryServices services)
        {
            _services = services;
        }
        public async Task<Response<IEnumerable<CategoryDTOs>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {

            var result = await _services.GetAllAsync();
            if (result != null)
            {
                return new Response<IEnumerable<CategoryDTOs>>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = "Category created successfully."
                };
            }
            throw new BadRequestException("Failed to create the Category. Please try again.");

        }
    }
}
