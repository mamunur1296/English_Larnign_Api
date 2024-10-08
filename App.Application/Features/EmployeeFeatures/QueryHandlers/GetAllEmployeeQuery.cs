using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.EmployeeFeatures.QueryHandlers
{
    public class GetAllEmployeeQuery : IRequest<Response<IEnumerable<EmployeeDTOs>>>
    {
    }

    public class GetAllEmployeeHandler : IRequestHandler<GetAllEmployeeQuery, Response<IEnumerable<EmployeeDTOs>>>
    {
        private readonly IEmployeeServices _services;

        public GetAllEmployeeHandler(IEmployeeServices services)
        {
            _services = services;
        }
        public async Task<Response<IEnumerable<EmployeeDTOs>>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {

            var result = await _services.GetAllAsync();
            if (result != null)
            {
                return new Response<IEnumerable<EmployeeDTOs>>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = "Employee created successfully."
                };
            }
            throw new BadRequestException("Failed to create the employee. Please try again.");

        }
    }
}
