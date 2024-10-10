using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.SubCategoryFeatures.QueryHandlers
{
    public class GetAllSubCategoryQuery : IRequest<Response<IEnumerable<SubCategoryDTOs>>>
    {
    }

    public class GetAllSubCategoryHandler : IRequestHandler<GetAllSubCategoryQuery, Response<IEnumerable<SubCategoryDTOs>>>
    {
        private readonly ISubCategoryServices _services;

        public GetAllSubCategoryHandler(ISubCategoryServices services)
        {
            _services = services;
        }
        public async Task<Response<IEnumerable<SubCategoryDTOs>>> Handle(GetAllSubCategoryQuery request, CancellationToken cancellationToken)
        {

            var result = await _services.GetAllAsync();
            if (result != null)
            {
                return new Response<IEnumerable<SubCategoryDTOs>>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = "Employee Sub Category successfully."
                };
            }
            throw new BadRequestException("Failed to create the Sub Category. Please try again.");

        }
    }
}
