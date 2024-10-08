using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.EmployeeFeatures.QueryHandlers
{
    public class GetByIdEmployeeQuery : IRequest<Response<EmployeeDTOs>>
    {
        public GetByIdEmployeeQuery(string id)
        {
            this.id = id;
        }

        public string id { get; private set; }
    }

    public class GetByIdEmployeeHandler : IRequestHandler<GetByIdEmployeeQuery, Response<EmployeeDTOs>>
    {

        private readonly IEmployeeServices _services;

        public GetByIdEmployeeHandler(IEmployeeServices services)
        {
            _services = services;
        }
        public async Task<Response<EmployeeDTOs>> Handle(GetByIdEmployeeQuery request, CancellationToken cancellationToken)
        {
            var result = await _services.GetByIdAsync(request.id);
            if (result != null)
            {
                return new Response<EmployeeDTOs>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message =  " Get Employee successfully."
                };
            }
            throw new BadRequestException("Failed to create the employee. Please try again.");
        }
    }
}
