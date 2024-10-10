using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.SubCategoryFeatures.QueryHandlers
{
    public class GetByIdSubCategoryQuery : IRequest<Response<SubCategoryDTOs>>
    {
        public GetByIdSubCategoryQuery(string id)
        {
            this.id = id;
        }

        public string id { get; private set; }
    }

    public class GetByIdSubCategoryHandler : IRequestHandler<GetByIdSubCategoryQuery, Response<SubCategoryDTOs>>
    {

        private readonly ISubCategoryServices _services;

        public GetByIdSubCategoryHandler(ISubCategoryServices services)
        {
            _services = services;
        }
        public async Task<Response<SubCategoryDTOs>> Handle(GetByIdSubCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _services.GetByIdAsync(request.id);
            if (result != null)
            {
                return new Response<SubCategoryDTOs>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = " Get Sub Category successfully."
                };
            }
            throw new BadRequestException("Failed to create the Sub Category. Please try again.");
        }
    }
}
