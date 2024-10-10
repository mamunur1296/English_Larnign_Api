using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;


namespace App.Application.Features.SubCategoryFeatures.QueryHandlers
{
    public class SearchBySubCategoryQuery : IRequest<Response<SearchBySubCategoryDTOs>>
    {
        public SearchBySubCategoryQuery(string subCategoryId, string verbid)
        {
            SubCategoryId = subCategoryId;
            Verbid = verbid;
        }

        public string SubCategoryId { get; private set; }
        public string Verbid { get; private set; }
    }

    public class SearchBySubCategoryHandler : IRequestHandler<SearchBySubCategoryQuery, Response<SearchBySubCategoryDTOs>>
    {

        private readonly ISubCategoryServices _services;

        public SearchBySubCategoryHandler(ISubCategoryServices services)
        {
            _services = services;
        }
        public async Task<Response<SearchBySubCategoryDTOs>> Handle(SearchBySubCategoryQuery request, CancellationToken cancellationToken)
        {
            var result = await _services.SearchBySubCategory(request.SubCategoryId,request.Verbid);
            if (result != null)
            {
                return new Response<SearchBySubCategoryDTOs>
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
