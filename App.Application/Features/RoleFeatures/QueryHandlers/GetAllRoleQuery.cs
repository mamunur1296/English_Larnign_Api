using App.Application.Common;
using App.Application.DTOs;
using App.Application.Exceptions;
using App.Application.Interfaces;
using MediatR;
using System.Net;

namespace App.Application.Features.RoleFeatures.QueryHandlers
{
    public class GetAllRoleQuery : IRequest<Response<IEnumerable<ApplicationRoleDTO>>>
    {
    }

    public class GetAllRoleHandler : IRequestHandler<GetAllRoleQuery, Response<IEnumerable<ApplicationRoleDTO>>>
    {
        private readonly IRoleService _services;

        public GetAllRoleHandler(IRoleService services)
        {
            _services = services;
        }
        public async Task<Response<IEnumerable<ApplicationRoleDTO>>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
        {

            var roles = await _services.GetRolesAsync();
            var result = roles.Select(ro => new ApplicationRoleDTO
            {
                RoleName=ro.roleName,
                Id=ro.id
            }).ToList();
            if (result != null)
            {
                return new Response<IEnumerable<ApplicationRoleDTO>>
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
