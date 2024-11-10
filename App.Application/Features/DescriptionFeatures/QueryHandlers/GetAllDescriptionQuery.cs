using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Features.EmployeeFeatures.QueryHandlers;
using App.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Features.DescriptionFeatures.QueryHandlers
{
    public class GetAllDescriptionQuery : IRequest<Response<IEnumerable<DescriptionDTOs>>>
    {
    }

    public class GetAllDescriptionHandler : IRequestHandler<GetAllDescriptionQuery, Response<IEnumerable<DescriptionDTOs>>>
    {
        private readonly IDescriptionServices _services;

        public GetAllDescriptionHandler(IDescriptionServices services)
        {
            _services = services;
        }
        public async Task<Response<IEnumerable<DescriptionDTOs>>> Handle(GetAllDescriptionQuery request, CancellationToken cancellationToken)
        {

            var result = await _services.GetAllAsync();
            if (result != null)
            {
                return new Response<IEnumerable<DescriptionDTOs>>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = "Description created successfully."
                };
            }
            throw new BadRequestException("Failed to create the Description. Please try again.");

        }
    }
}
