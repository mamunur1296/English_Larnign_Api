using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Features.EmployeeFeatures.QueryHandlers;
using App.Application.Interfaces;
using MediatR;
using System.Net;


namespace App.Application.Features.UserFeatures.QueryHandlers
{
    public class GetByIdUserQuery : IRequest<Response<UserDTO>>
    {
        public GetByIdUserQuery(string id)
        {
            this.id = id;
        }

        public string id { get; private set; }
    }

    public class GetByIdUserHandler : IRequestHandler<GetByIdUserQuery, Response<UserDTO>>
    {

        private readonly IUserService _services;

        public GetByIdUserHandler(IUserService services)
        {
            _services = services;
        }
      

        public async Task<Response<UserDTO>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var result = await _services.GetUserDetailsAsync(request.id);
            if (result != null)
            {
                return new Response<UserDTO>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = result,
                    Message = " Get Employee successfully."
                };
            }
            throw new BadRequestException("Failed to create the employee. Please try again.");
        }
    }
}
