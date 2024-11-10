using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Features.EmployeeFeatures.QueryHandlers;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.DescriptionFeatures.QueryHandlers
{
    public class GetByIdDescriptionQuery : IRequest<Response<DescriptionDTOs>>
    {
        public GetByIdDescriptionQuery(string id)
        {
            this.id = id;
        }

        public string id { get; private set; }
    }

    public class GetByIdDescriptionHandler : IRequestHandler<GetByIdDescriptionQuery, Response<DescriptionDTOs>>
    {

        private readonly IDescriptionServices _services;

        public GetByIdDescriptionHandler(IDescriptionServices services)
        {
            _services = services;
        }
        public async Task<Response<DescriptionDTOs>> Handle(GetByIdDescriptionQuery request, CancellationToken cancellationToken)
        {
            var result = await _services.GetByIdAsync(request.id);
            if (result != null)
            {
                return new Response<DescriptionDTOs>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = " Get Description successfully."
                };
            }
            throw new BadRequestException("Failed to create the Description. Please try again.");
        }
    }
}
