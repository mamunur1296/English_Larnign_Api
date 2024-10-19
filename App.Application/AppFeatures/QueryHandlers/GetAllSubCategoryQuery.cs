using App.Application.AppFeatures.AppDtos;
using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.AppFeatures.QueryHandlers
{
    public class GetAllAppSubCategoryQuery : IRequest<Response<IEnumerable<SubCategoryDTOs>>>
    {
    }

    public class GetAllAppSubCategoryHandler : IRequestHandler<GetAllAppSubCategoryQuery, Response<IEnumerable<SubCategoryDTOs>>>
    {
        private readonly ISubCategoryServices _services;

        public GetAllAppSubCategoryHandler(ISubCategoryServices services)
        {
            _services = services;
        }
        public async Task<Response<IEnumerable<SubCategoryDTOs>>> Handle(GetAllAppSubCategoryQuery request, CancellationToken cancellationToken)
        {

            var result = await _services.GetAllAsync();
            if (result != null)
            {
                return new Response<IEnumerable<SubCategoryDTOs>>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = "Sub Category successfully."
                };
            }
            throw new BadRequestException("Failed to create the Sub Category. Please try again.");

        }
    }
}
