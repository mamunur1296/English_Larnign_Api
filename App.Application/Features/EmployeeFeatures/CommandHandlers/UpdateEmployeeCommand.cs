using App.Application.Common;
using App.Application.Exceptions;
using App.Application.Interfaces;
using FluentValidation;
using MediatR;
using System.Net;

namespace App.Application.Features.EmployeeFeatures.CommandHandlers
{
    public class UpdateEmployeeCommand : IRequest<Response<string>>
    {
        public string ? id { get;  set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string Phone { get; set; }
        public string? UpdatedBy { get; set; }

    }

    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, Response<string>>
    {
        private readonly IEmployeeServices _services;
        private readonly IValidator<UpdateEmployeeCommand> _validator;

        public UpdateEmployeeHandler(IEmployeeServices services, IValidator<UpdateEmployeeCommand> validator)
        {
            _services = services;
            _validator = validator;
        }

        public async Task<Response<string>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidatException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }
            var result = await _services.UpdateAsync(request);

            if (result.Success)
            {
                return new Response<string>
                {
                    Success = true,
                    Status = HttpStatusCode.Created, // You can set the status code here
                    id = result.id,
                    Data = "Success",
                    Message = "User Update successfully."
                };
            }
            throw new BadRequestException("Failed to Update the User. Please try again.");
        }
    }
}
