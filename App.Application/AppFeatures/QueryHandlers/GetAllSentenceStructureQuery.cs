using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.AppFeatures.QueryHandlers
{
    public class GetAllSentenceStructureAppQuery : IRequest<Response<IEnumerable<SentenceStructureDTOs>>>
    {
        public string SubCatagoryID { get; }
        public string FormId { get; }
        public int? PageSize { get; }
        public int? PageNumber { get; }

        public GetAllSentenceStructureAppQuery(string subCatagoryId, string formId, int? pageSize, int? pageNumber)
        {
            SubCatagoryID = subCatagoryId;
            FormId = formId;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
    }



    public class GetAllSentenceStructureHandler : IRequestHandler<GetAllSentenceStructureAppQuery, Response<IEnumerable<SentenceStructureDTOs>>>
    {
        private readonly ISentenceStructureServices _services;

        public GetAllSentenceStructureHandler(ISentenceStructureServices services)
        {
            _services = services;
        }

        public async Task<Response<IEnumerable<SentenceStructureDTOs>>> Handle(GetAllSentenceStructureAppQuery request, CancellationToken cancellationToken)
        {
            var (result,PageCount) = await _services.GetAllFilterAsync(request.SubCatagoryID, request.FormId, request.PageSize ?? 0, request.PageNumber ?? 0); // Fetch all data first (or implement a more optimized query directly with pagination)

            if (result != null && result.Any())
            {
                return new Response<IEnumerable<SentenceStructureDTOs>>
                {
                    Success = true,
                    Status = HttpStatusCode.OK,
                    Data = result,
                    Message = $"Total Page Count: {PageCount}"
                };
            }

            throw new NotFoundException("No Sentence Structures found with the provided filters.");
        }
    }

}
