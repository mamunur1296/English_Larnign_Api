using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using FluentValidation;
using MediatR;
using System.Net;

namespace App.Application.Features.RoleFeatures.CommandHandlers
{
    public class CreateRoleCommand : IRequest<Response<string>>
    {
        public string RoleName { get; set; }
        
    }
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, Response<string>>
    {
        private readonly IRoleService _services;
        private readonly IValidator<CreateRoleCommand> _validator;

        public CreateRoleHandler(IRoleService services, IValidator<CreateRoleCommand> validator)
        {
            _services = services;
            _validator = validator;
        }

        public async Task<Response<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidatException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            var result = await _services.CreateRoleAsync(request.RoleName);

            if (result)
            {
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    Data = "Success",
                    Message = "Role created successfully."
                };
            }
            throw new BadRequestException("Failed to create the Role. Please try again.");
        }
    }
}
