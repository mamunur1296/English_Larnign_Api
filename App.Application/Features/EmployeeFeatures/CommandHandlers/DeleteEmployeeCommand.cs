using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.EmployeeFeatures.CommandHandlers
{
    public class DeleteEmployeeCommand : IRequest<Response<string>>
    {
        public DeleteEmployeeCommand(string id)
        {
            this.id = id;
        }

        public string id { get; private set; }
    }
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, Response<string>>
    {

        private readonly IEmployeeServices _services;


        public DeleteEmployeeHandler(IEmployeeServices services)
        {
            _services = services;
        }

        public async Task<Response<string>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var result = await _services.DeleteAsync(request.id);
            if (result.Success)
            {
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.OK, // You can set the status code here
                    id = result.id,
                    Data = "Success",
                    Message = "Employee Delete successfully."
                };
            }
            throw new BadRequestException("Failed to Delete the employee. Please try again.");
        }
    }
}
